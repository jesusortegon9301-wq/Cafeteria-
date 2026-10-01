namespace Cafeteria.Pagos.Externos;

/// <summary>API SIMULADA de Daviplata. No hace ninguna llamada real. Retorna un código: "00" = aprobado.</summary>
public class DaviplataApi
{
    private readonly decimal _fondosDisponibles;

    public DaviplataApi(decimal fondosDisponibles = 30000m)
    {
        _fondosDisponibles = fondosDisponibles;
    }

    public string Transferir(string cuenta, decimal monto) => monto <= _fondosDisponibles ? "00" : "51";
}
