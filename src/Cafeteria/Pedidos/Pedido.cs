using Cafeteria.Productos;

namespace Cafeteria.Pedidos;

/// <summary>
/// Guarda el estado, el total y el mensaje de un pedido, y protege sus transiciones:
/// CREADO → PERSONALIZADO → PAGO_PROCESADO → CONFIRMADO. Cualquier fallo lleva a RECHAZADO.
/// Cada transición devuelve false (sin cambiar nada) si se intenta fuera de orden.
/// </summary>
public class Pedido
{
    public Producto? Producto { get; private set; }
    public decimal Total { get; private set; }
    public EstadoPedido Estado { get; private set; } = EstadoPedido.Creado;
    public string Mensaje { get; private set; } = "Pedido creado.";

    public string Descripcion => Producto is null
        ? "(sin producto)"
        : $"{Producto.Nombre} - {Producto.Descripcion}";

    public string EstadoTexto => Estado switch
    {
        EstadoPedido.Creado => "CREADO",
        EstadoPedido.Personalizado => "PERSONALIZADO",
        EstadoPedido.PagoProcesado => "PAGO_PROCESADO",
        EstadoPedido.Confirmado => "CONFIRMADO",
        EstadoPedido.Rechazado => "RECHAZADO",
        _ => Estado.ToString()
    };

    public bool Personalizar(Producto producto)
    {
        if (Estado != EstadoPedido.Creado) return false;

        Producto = producto;
        Total = producto.Precio;
        Estado = EstadoPedido.Personalizado;
        Mensaje = "Pedido personalizado.";
        return true;
    }

    public bool MarcarPagoProcesado()
    {
        if (Estado != EstadoPedido.Personalizado) return false;

        Estado = EstadoPedido.PagoProcesado;
        Mensaje = "Pago procesado.";
        return true;
    }

    /// <summary>RF-12: solo se puede confirmar si el pago ya fue procesado (aprobado).</summary>
    public bool Confirmar()
    {
        if (Estado != EstadoPedido.PagoProcesado) return false;

        Estado = EstadoPedido.Confirmado;
        Mensaje = "Pedido confirmado.";
        return true;
    }

    public bool Rechazar(string motivo)
    {
        if (Estado == EstadoPedido.Confirmado) return false;

        Estado = EstadoPedido.Rechazado;
        Mensaje = motivo;
        return true;
    }

    /// <summary>RF-14: texto con el resultado final del pedido, listo para mostrar al usuario.</summary>
    public string ObtenerResumen() =>
        $"Estado:   {EstadoTexto}{Environment.NewLine}" +
        $"Producto: {Descripcion}{Environment.NewLine}" +
        $"Total:    {Total:N0} COP{Environment.NewLine}" +
        $"Mensaje:  {Mensaje}";
}
