using Cafeteria.Productos;

namespace Cafeteria.Complementos;

/// <summary>
/// DECORATOR (patrón estructural).
/// Un decorador ES un <see cref="Producto"/> y TIENE otro Producto (el envuelto).
/// Por eso se pueden anidar: Cafe → Leche → Crema → Caramelo, sin crear una clase por combinación.
/// </summary>
public abstract class ProductoDecorator : Producto
{
    private readonly Producto _producto;

    protected ProductoDecorator(Producto producto)
    {
        _producto = producto ?? throw new ArgumentNullException(nameof(producto));
    }

    protected abstract string NombreComplemento { get; }
    protected abstract decimal PrecioComplemento { get; }

    public override string Nombre => _producto.Nombre;
    public override string Descripcion => $"{_producto.Descripcion} + {NombreComplemento}";
    public override decimal Precio => _producto.Precio + PrecioComplemento;
}
