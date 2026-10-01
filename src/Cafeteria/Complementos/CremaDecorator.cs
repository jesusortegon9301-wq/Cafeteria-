using Cafeteria.Productos;

namespace Cafeteria.Complementos;

public class CremaDecorator : ProductoDecorator
{
    public CremaDecorator(Producto producto) : base(producto) { }

    protected override string NombreComplemento => "Crema";
    protected override decimal PrecioComplemento => 1500m;
}
