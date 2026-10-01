using Cafeteria.Productos;

namespace Cafeteria.Creadores;

public class CafeCreator : ProductoCreator
{
    public override Producto CrearProducto() => new Cafe();
}
