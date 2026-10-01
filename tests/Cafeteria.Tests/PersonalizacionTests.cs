using Cafeteria.Complementos;
using Cafeteria.Pedidos;
using Cafeteria.Productos;

namespace Cafeteria.Tests;

/// <summary>Especificación "personalizacion": Decorator y cálculo de precio (CA-04, CA-05, CA-06).</summary>
public class PersonalizacionTests
{
    [Fact]
    public void CA04_ProductoSinComplementos_PrecioIgualAlBase()
    {
        Producto cafe = new Cafe();

        Assert.Equal(3000m, cafe.Precio);
    }

    [Fact]
    public void CA05_AgregarUnComplemento_AumentaElPrecio()
    {
        Producto cafeConLeche = new LecheDecorator(new Cafe());

        Assert.Equal(4000m, cafeConLeche.Precio);
    }

    [Fact]
    public void CA05_CadaComplemento_SumaSuPrecioConfigurado()
    {
        Assert.Equal(3000m + 1000m, new LecheDecorator(new Cafe()).Precio);
        Assert.Equal(3000m + 1500m, new CremaDecorator(new Cafe()).Precio);
        Assert.Equal(3000m + 1200m, new CarameloDecorator(new Cafe()).Precio);
        Assert.Equal(3000m + 500m, new AzucarDecorator(new Cafe()).Precio);
    }

    [Fact]
    public void CA06_VariosComplementos_SumaTodos()
    {
        Producto pedido = new CarameloDecorator(new CremaDecorator(new LecheDecorator(new Cafe())));

        Assert.Equal(6700m, pedido.Precio); // 3000 + 1000 + 1500 + 1200
    }

    [Fact]
    public void Decorator_ElComplementoEsUnProducto_YConservaElNombreBase()
    {
        Producto conLeche = new LecheDecorator(new Capuchino());

        Assert.IsAssignableFrom<Producto>(conLeche);
        Assert.Equal("Capuchino", conLeche.Nombre);
    }

    [Fact]
    public void Decorator_LaDescripcionReflejaLosComplementos()
    {
        Producto pedido = new CremaDecorator(new LecheDecorator(new Cafe()));

        Assert.Contains("Leche", pedido.Descripcion);
        Assert.Contains("Crema", pedido.Descripcion);
    }

    [Fact]
    public void Facade_AplicaLosComplementosPedidos_YCalculaElTotal()
    {
        var facade = new PedidoFacade();

        Pedido pedido = facade.RealizarPedido("Cafe", new[] { "Leche", "Crema", "Caramelo" }, "Nequi");

        Assert.Equal(6700m, pedido.Total);
    }

    [Fact]
    public void ComplementoInvalido_RechazaElPedido()
    {
        var facade = new PedidoFacade();

        Pedido pedido = facade.RealizarPedido("Cafe", new[] { "Canela" }, "Nequi");

        Assert.Equal(EstadoPedido.Rechazado, pedido.Estado);
        Assert.Contains("Canela", pedido.Mensaje);
    }
}
