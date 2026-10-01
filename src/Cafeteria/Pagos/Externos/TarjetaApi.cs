namespace Cafeteria.Pagos.Externos;

/// <summary>API SIMULADA de tarjeta. No hace ninguna llamada real. Retorna 0 si se deniega, o un código de autorización.</summary>
public class TarjetaApi
{
    private const int CodigoAutorizacion = 100001;

    private readonly decimal _fondosDisponibles;

    public TarjetaApi(decimal fondosDisponibles = 100000m)
    {
        _fondosDisponibles = fondosDisponibles;
    }

    public int Cobrar(string numero, decimal monto) => monto <= _fondosDisponibles ? CodigoAutorizacion : 0;
}
