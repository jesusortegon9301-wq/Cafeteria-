using Cafeteria.Productos;

namespace Cafeteria.Creadores;

public class ChocolateCreator : ProductoCreator
{
    public override Producto CrearProducto() => new Chocolate();
}
