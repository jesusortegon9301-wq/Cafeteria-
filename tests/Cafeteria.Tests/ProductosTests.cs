using Cafeteria.Creadores;
using Cafeteria.Pagos;
using Cafeteria.Pedidos;
using Cafeteria.Productos;

namespace Cafeteria.Tests;

/// <summary>Especificación "productos": Factory Method (CA-01, CA-02, CA-03).</summary>
public class ProductosTests
{
    [Fact]
    public void CA01_CrearCafe_DevuelveCafeValido()
    {
        ProductoCreator creador = new CafeCreator();

        Producto producto = creador.CrearProducto();

        Assert.IsType<Cafe>(producto);
        Assert.Equal("Café", producto.Nombre);
        Assert.Equal(3000m, producto.Precio);
        Assert.False(string.IsNullOrWhiteSpace(producto.Descripcion));
    }

    public static IEnumerable<object[]> OtrosCreadores => new[]
    {
        new object[] { new CapuchinoCreator(), "Capuchino", 4500m },
        new object[] { new LatteCreator(), "Latte", 5000m },
        new object[] { new ChocolateCreator(), "Chocolate", 4000m }
    };

    [Theory]
    [MemberData(nameof(OtrosCreadores))]
    public void CA02_CrearOtrosProductos_DevuelveElProductoCorrespondiente(
        ProductoCreator creador, string nombreEsperado, decimal precioEsperado)
    {
        Producto producto = creador.CrearProducto();

        Assert.Equal(nombreEsperado, producto.Nombre);
        Assert.Equal(precioEsperado, producto.Precio);
        Assert.False(string.IsNullOrWhiteSpace(producto.Descripcion));
    }

    [Fact]
    public void CA03_ProductoInvalido_RechazaDeFormaControlada_SinCobrar()
    {
        var pago = new PagoFalso("Nequi", aprueba: true);
        var facade = new PedidoFacade(new Dictionary<string, IPayment> { ["Nequi"] = pago });

        Pedido pedido = facade.RealizarPedido("Espresso", Array.Empty<string>(), "Nequi");

        Assert.Equal(EstadoPedido.Rechazado, pedido.Estado);
        Assert.Contains("producto", pedido.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(0, pago.Llamadas);
    }
}
