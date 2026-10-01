namespace Cafeteria.Pagos;

/// <summary>Resultado uniforme de un pago, sin importar el medio utilizado.</summary>
public record ResultadoPago(bool Aprobado, string Mensaje);
