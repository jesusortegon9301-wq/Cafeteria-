using Cafeteria.Pagos.Externos;

namespace Cafeteria.Pagos;

/// <summary>ADAPTER: traduce <see cref="DaviplataApi"/> (devuelve un código) a <see cref="IPayment"/>.</summary>
public class DaviplataAdapter : IPayment
{
    private readonly DaviplataApi _api;
    private readonly string _cuenta;

    public DaviplataAdapter() : this(new DaviplataApi()) { }

    public DaviplataAdapter(DaviplataApi api, string cuenta = "3109876543")
    {
        _api = api;
        _cuenta = cuenta;
    }

    public string Nombre => "Daviplata";

    public ResultadoPago Pagar(decimal monto)
    {
        string codigo = _api.Transferir(_cuenta, monto);

        return codigo == "00"
            ? new ResultadoPago(true, $"Pago de {monto} aprobado con Daviplata.")
            : new ResultadoPago(false, "Daviplata rechazó el pago: fondos insuficientes.");
    }
}
