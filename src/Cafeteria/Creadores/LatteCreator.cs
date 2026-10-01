using Cafeteria.Productos;

namespace Cafeteria.Creadores;

public class LatteCreator : ProductoCreator
{
    public override Producto CrearProducto() => new Latte();
}
