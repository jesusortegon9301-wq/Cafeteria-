using Cafeteria.Pagos.Externos;

namespace Cafeteria.Pagos;

/// <summary>ADAPTER: traduce <see cref="NequiApi"/> (devuelve bool) a <see cref="IPayment"/>.</summary>
public class NequiAdapter : IPayment
{
    private readonly NequiApi _api;
    private readonly string _celular;

    public NequiAdapter() : this(new NequiApi()) { }

    public NequiAdapter(NequiApi api, string celular = "3001234567")
    {
        _api = api;
        _celular = celular;
    }

    public string Nombre => "Nequi";

    public ResultadoPago Pagar(decimal monto)
    {
        bool aprobado = _api.EnviarPago(_celular, monto);

        return aprobado
            ? new ResultadoPago(true, $"Pago de {monto} aprobado con Nequi.")
            : new ResultadoPago(false, "Nequi rechazó el pago: fondos insuficientes.");
    }
}
