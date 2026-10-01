using Cafeteria.Pagos.Externos;

namespace Cafeteria.Pagos;

/// <summary>ADAPTER: traduce <see cref="TarjetaApi"/> (devuelve un entero) a <see cref="IPayment"/>.</summary>
public class TarjetaAdapter : IPayment
{
    private readonly TarjetaApi _api;
    private readonly string _numero;

    public TarjetaAdapter() : this(new TarjetaApi()) { }

    public TarjetaAdapter(TarjetaApi api, string numero = "4111111111111111")
    {
        _api = api;
        _numero = numero;
    }

    public string Nombre => "Tarjeta";

    public ResultadoPago Pagar(decimal monto)
    {
        int autorizacion = _api.Cobrar(_numero, monto);

        return autorizacion != 0
            ? new ResultadoPago(true, $"Pago de {monto} aprobado con Tarjeta (autorización {autorizacion}).")
            : new ResultadoPago(false, "La tarjeta rechazó el pago: cupo insuficiente.");
    }
}
