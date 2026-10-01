using Cafeteria.Pagos;
using Cafeteria.Productos;

namespace Cafeteria.Pedidos;

/// <summary>
/// Decide si un pedido es válido. Devuelve la lista de errores (vacía = válido).
/// Se usa en dos momentos: antes de cobrar (RF-09, RF-10) y después de cobrar (RF-11).
/// </summary>
public class ValidadorPedido
{
    public IReadOnlyList<string> ValidarAntesDePago(Producto? producto, IPayment? medioPago)
    {
        var errores = new List<string>();

        if (producto is null)
            errores.Add("El producto solicitado no es soportado.");

        if (medioPago is null)
            errores.Add("El medio de pago solicitado no es soportado.");

        return errores;
    }

    public IReadOnlyList<string> ValidarPago(ResultadoPago resultado)
    {
        var errores = new List<string>();

        if (!resultado.Aprobado)
            errores.Add($"Pago no aprobado. {resultado.Mensaje}");

        return errores;
    }
}
