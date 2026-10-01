namespace Cafeteria.Pagos;

/// <summary>
/// Interfaz común de pago (la interfaz "objetivo" del patrón ADAPTER).
/// El código principal solo conoce esta interfaz, no las diferencias entre Nequi, Daviplata y Tarjeta.
/// </summary>
public interface IPayment
{
    string Nombre { get; }
    ResultadoPago Pagar(decimal monto);
}
