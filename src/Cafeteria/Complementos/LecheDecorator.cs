using Cafeteria.Productos;

namespace Cafeteria.Complementos;

public class LecheDecorator : ProductoDecorator
{
    public LecheDecorator(Producto producto) : base(producto) { }

    protected override string NombreComplemento => "Leche";
    protected override decimal PrecioComplemento => 1000m;
}
