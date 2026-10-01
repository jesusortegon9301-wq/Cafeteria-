using Cafeteria.Productos;

namespace Cafeteria.Complementos;

public class CarameloDecorator : ProductoDecorator
{
    public CarameloDecorator(Producto producto) : base(producto) { }

    protected override string NombreComplemento => "Caramelo";
    protected override decimal PrecioComplemento => 1200m;
}
