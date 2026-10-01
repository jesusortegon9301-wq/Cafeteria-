namespace Cafeteria.Pagos.Externos;

/// <summary>API SIMULADA de Nequi. No hace ninguna llamada real. Retorna bool.</summary>
public class NequiApi
{
    private readonly decimal _fondosDisponibles;

    public NequiApi(decimal fondosDisponibles = 50000m)
    {
        _fondosDisponibles = fondosDisponibles;
    }

    public bool EnviarPago(string celular, decimal valor) => valor <= _fondosDisponibles;
}
