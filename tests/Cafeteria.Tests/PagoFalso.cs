using Cafeteria.Pagos;

namespace Cafeteria.Tests;

/// <summary>Medio de pago de prueba: cuenta cuántas veces se le cobró y aprueba o rechaza según se configure.</summary>
internal class PagoFalso : IPayment
{
    private readonly bool _aprueba;

    public PagoFalso(string nombre, bool aprueba)
    {
        Nombre = nombre;
        _aprueba = aprueba;
    }

    public string Nombre { get; }
    public int Llamadas { get; private set; }

    public ResultadoPago Pagar(decimal monto)
    {
        Llamadas++;
        return new ResultadoPago(_aprueba, _aprueba ? "Aprobado (prueba)." : "Rechazado (prueba).");
    }
}
