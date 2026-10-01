using Cafeteria.Productos;

namespace Cafeteria.Creadores;

/// <summary>
/// FACTORY METHOD (patrón creacional).
/// Declara el método fábrica <see cref="CrearProducto"/>; cada subclase decide qué producto
/// concreto crea. Así el código cliente solo conoce <see cref="Producto"/> y este creador.
/// </summary>
public abstract class ProductoCreator
{
    public abstract Producto CrearProducto();
}
