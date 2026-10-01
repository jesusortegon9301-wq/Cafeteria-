using Cafeteria.Pagos;
using Cafeteria.Pagos.Externos;
using Cafeteria.Pedidos;
using Cafeteria.Productos;

namespace Cafeteria.Tests;

/// <summary>Especificación "pedidos": Facade, validaciones y estados (CA-08, CA-09, CA-10).</summary>
public class PedidosTests
{
    [Fact]
    public void CA09_PagoAprobado_ElPedidoQuedaConfirmado()
    {
        var facade = new PedidoFacade();

        Pedido pedido = facade.RealizarPedido("Cafe", new[] { "Leche" }, "Nequi");

        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
        Assert.Equal("CONFIRMADO", pedido.EstadoTexto);
        Assert.Equal(4000m, pedido.Total);
    }

    [Theory]
    [InlineData("Nequi")]
    [InlineData("Daviplata")]
    [InlineData("Tarjeta")]
    public void CA09_LosTresMediosDePago_ConfirmanElPedido(string medioPago)
    {
        var facade = new PedidoFacade();

        Pedido pedido = facade.RealizarPedido("Latte", Array.Empty<string>(), medioPago);

        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
    }

    [Fact]
    public void CA08_PagoRechazado_ElPedidoNoQuedaConfirmado()
    {
        var medios = new Dictionary<string, IPayment>
        {
            ["Nequi"] = new NequiAdapter(new NequiApi(1000m))
        };
        var facade = new PedidoFacade(medios);

        Pedido pedido = facade.RealizarPedido("Cafe", new[] { "Leche" }, "Nequi");

        Assert.NotEqual(EstadoPedido.Confirmado, pedido.Estado);
        Assert.Equal(EstadoPedido.Rechazado, pedido.Estado);
        Assert.Contains("rechaz", pedido.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CA10_Facade_EjecutaElProcesoCompletoSinConocerLosDetalles()
    {
        // El cliente solo usa PedidoFacade y recibe un Pedido: no toca creadores, decoradores ni adaptadores.
        var facade = new PedidoFacade();

        Pedido pedido = facade.RealizarPedido("Capuchino", new[] { "Leche", "Azucar" }, "Tarjeta");

        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
        Assert.Equal(6000m, pedido.Total); // 4500 + 1000 + 500
        Assert.Contains("Capuchino", pedido.Descripcion);
        Assert.Contains("Leche", pedido.Descripcion);
    }

    [Fact]
    public void SeleccionDeMedioDePago_NoDistingueMayusculas()
    {
        var facade = new PedidoFacade();

        Pedido pedido = facade.RealizarPedido("cafe", Array.Empty<string>(), "nequi");

        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
    }

    [Fact]
    public void MedioDePagoInexistente_RechazaElPedido_SinCobrar()
    {
        var pago = new PagoFalso("Nequi", aprueba: true);
        var facade = new PedidoFacade(new Dictionary<string, IPayment> { ["Nequi"] = pago });

        Pedido pedido = facade.RealizarPedido("Cafe", Array.Empty<string>(), "Bitcoin");

        Assert.Equal(EstadoPedido.Rechazado, pedido.Estado);
        Assert.Contains("pago", pedido.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(0, pago.Llamadas);
    }

    [Fact]
    public void Estados_CaminoExitoso_CreadoPersonalizadoPagoProcesadoConfirmado()
    {
        var pedido = new Pedido();
        Assert.Equal(EstadoPedido.Creado, pedido.Estado);

        Assert.True(pedido.Personalizar(new Cafe()));
        Assert.Equal(EstadoPedido.Personalizado, pedido.Estado);

        Assert.True(pedido.MarcarPagoProcesado());
        Assert.Equal(EstadoPedido.PagoProcesado, pedido.Estado);

        Assert.True(pedido.Confirmar());
        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
    }

    [Fact]
    public void Confirmar_FueraDeOrden_NoConfirmaElPedido()
    {
        var pedido = new Pedido();
        pedido.Personalizar(new Cafe());

        bool confirmado = pedido.Confirmar(); // aún no hay pago procesado

        Assert.False(confirmado);
        Assert.Equal(EstadoPedido.Personalizado, pedido.Estado);
    }

    [Fact]
    public void ValidadorPedido_ValidaLoNecesarioAntesYDespuesDelPago()
    {
        var validador = new ValidadorPedido();

        Assert.Empty(validador.ValidarAntesDePago(new Cafe(), new NequiAdapter()));
        Assert.Equal(2, validador.ValidarAntesDePago(null, null).Count);
        Assert.Empty(validador.ValidarPago(new ResultadoPago(true, "ok")));
        Assert.Single(validador.ValidarPago(new ResultadoPago(false, "no")));
    }

    [Fact]
    public void RF14_ResultadoFinal_MuestraEstadoDescripcionTotalYMensaje()
    {
        var facade = new PedidoFacade();
        Pedido pedido = facade.RealizarPedido("Cafe", new[] { "Leche" }, "Nequi");

        string resumen = pedido.ObtenerResumen();

        Assert.Contains("CONFIRMADO", resumen);
        Assert.Contains("Leche", resumen);
        Assert.Contains(4000m.ToString("N0"), resumen);
        Assert.Contains("Pedido confirmado.", resumen);
    }
}
