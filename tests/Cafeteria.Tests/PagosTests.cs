using Cafeteria.Pagos;
using Cafeteria.Pagos.Externos;

namespace Cafeteria.Tests;

/// <summary>Especificación "pagos": Adapter e interfaz común (CA-07, CA-08).</summary>
public class PagosTests
{
    [Fact]
    public void CA07_Nequi_ProcesaElPagoMedianteLaInterfazComun()
    {
        IPayment medio = new NequiAdapter();

        ResultadoPago resultado = medio.Pagar(4000m);

        Assert.Equal("Nequi", medio.Nombre);
        Assert.True(resultado.Aprobado);
    }

    [Fact]
    public void CA07_Daviplata_ProcesaElPagoMedianteLaInterfazComun()
    {
        IPayment medio = new DaviplataAdapter();

        ResultadoPago resultado = medio.Pagar(4000m);

        Assert.Equal("Daviplata", medio.Nombre);
        Assert.True(resultado.Aprobado);
    }

    [Fact]
    public void CA07_Tarjeta_ProcesaElPagoMedianteLaInterfazComun()
    {
        IPayment medio = new TarjetaAdapter();

        ResultadoPago resultado = medio.Pagar(4000m);

        Assert.Equal("Tarjeta", medio.Nombre);
        Assert.True(resultado.Aprobado);
    }

    public static IEnumerable<object[]> MediosSinFondos => new[]
    {
        new object[] { new NequiAdapter(new NequiApi(1000m)) },
        new object[] { new DaviplataAdapter(new DaviplataApi(1000m)) },
        new object[] { new TarjetaAdapter(new TarjetaApi(1000m)) }
    };

    [Theory]
    [MemberData(nameof(MediosSinFondos))]
    public void CA08_FondosInsuficientes_ElPagoEsRechazado(IPayment medio)
    {
        ResultadoPago resultado = medio.Pagar(4000m);

        Assert.False(resultado.Aprobado);
        Assert.False(string.IsNullOrWhiteSpace(resultado.Mensaje));
    }

    [Fact]
    public void PagoSimulado_MismoMontoYFondos_DaSiempreElMismoResultado()
    {
        IPayment medio = new NequiAdapter(new NequiApi(5000m));

        ResultadoPago primero = medio.Pagar(4000m);
        ResultadoPago segundo = medio.Pagar(4000m);

        Assert.Equal(primero, segundo);
    }
}
