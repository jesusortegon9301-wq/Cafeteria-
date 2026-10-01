using Cafeteria.Complementos;
using Cafeteria.Creadores;
using Cafeteria.Pagos;
using Cafeteria.Productos;

namespace Cafeteria.Pedidos;

/// <summary>
/// FACADE (patrón estructural).
/// Ofrece una sola operación, <see cref="RealizarPedido"/>, que coordina en orden Factory Method,
/// Decorator, Adapter y la validación. El cliente no conoce ninguno de esos detalles.
/// El Facade solo COORDINA: no calcula precios, no valida y no procesa pagos por sí mismo.
/// </summary>
public class PedidoFacade
{
    // Tablas de búsqueda por nombre (configuración simple, no son fábricas).
    private readonly Dictionary<string, ProductoCreator> _creadores = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Cafe"] = new CafeCreator(),
        ["Capuchino"] = new CapuchinoCreator(),
        ["Latte"] = new LatteCreator(),
        ["Chocolate"] = new ChocolateCreator()
    };

    private readonly Dictionary<string, Func<Producto, Producto>> _complementos = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Leche"] = p => new LecheDecorator(p),
        ["Crema"] = p => new CremaDecorator(p),
        ["Caramelo"] = p => new CarameloDecorator(p),
        ["Azucar"] = p => new AzucarDecorator(p)
    };

    private readonly Dictionary<string, IPayment> _mediosDePago;
    private readonly ValidadorPedido _validador = new();

    /// <summary>Usa los tres medios de pago simulados con sus fondos por defecto.</summary>
    public PedidoFacade() : this(new Dictionary<string, IPayment>
    {
        ["Nequi"] = new NequiAdapter(),
        ["Daviplata"] = new DaviplataAdapter(),
        ["Tarjeta"] = new TarjetaAdapter()
    })
    {
    }

    /// <summary>Permite inyectar los medios de pago (útil en pruebas para forzar un rechazo).</summary>
    public PedidoFacade(IReadOnlyDictionary<string, IPayment> mediosDePago)
    {
        _mediosDePago = new Dictionary<string, IPayment>(mediosDePago, StringComparer.OrdinalIgnoreCase);
    }

    public Pedido RealizarPedido(string producto, IEnumerable<string> complementos, string medioPago)
    {
        var pedido = new Pedido();

        // 1-2. Factory Method: crear el producto a partir de su nombre.
        Producto? productoBase = Buscar(_creadores, producto)?.CrearProducto();

        // 3-4. Decorator: envolver el producto con cada complemento pedido.
        if (productoBase is not null)
        {
            Producto decorado = productoBase;

            foreach (string nombre in complementos)
            {
                Func<Producto, Producto>? envolver = Buscar(_complementos, nombre);

                if (envolver is null)
                {
                    pedido.Rechazar($"El complemento '{nombre}' no es soportado.");
                    return pedido;
                }

                decorado = envolver(decorado);
            }

            pedido.Personalizar(decorado);
        }

        // 5. Adapter: obtener el medio de pago a través de la interfaz común.
        IPayment? medio = Buscar(_mediosDePago, medioPago);

        // 6. Validar antes de cobrar (RF-09, RF-10). "medio is null" solo ayuda al compilador.
        IReadOnlyList<string> errores = _validador.ValidarAntesDePago(productoBase, medio);
        if (errores.Count > 0 || medio is null)
        {
            pedido.Rechazar(string.Join(" ", errores));
            return pedido;
        }

        // 7. Procesar el pago (simulado).
        ResultadoPago resultado = medio.Pagar(pedido.Total);

        // 8. Validar el resultado del pago (RF-11).
        errores = _validador.ValidarPago(resultado);
        if (errores.Count > 0)
        {
            pedido.Rechazar(string.Join(" ", errores));
            return pedido;
        }

        // 9. Pago aprobado: PAGO_PROCESADO y luego CONFIRMADO (RF-12).
        pedido.MarcarPagoProcesado();
        pedido.Confirmar();
        return pedido;
    }

    private static T? Buscar<T>(Dictionary<string, T> tabla, string? nombre) where T : class
    {
        return nombre is not null && tabla.TryGetValue(nombre, out T? valor) ? valor : null;
    }
}
