using Cafeteria.Productos;

namespace Cafeteria.Complementos;

public class AzucarDecorator : ProductoDecorator
{
    public AzucarDecorator(Producto producto) : base(producto) { }

    protected override string NombreComplemento => "Azúcar";
    protected override decimal PrecioComplemento => 500m;
}
