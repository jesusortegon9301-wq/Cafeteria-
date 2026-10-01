using Cafeteria.Productos;

namespace Cafeteria.Creadores;

public class CapuchinoCreator : ProductoCreator
{
    public override Producto CrearProducto() => new Capuchino();
}
