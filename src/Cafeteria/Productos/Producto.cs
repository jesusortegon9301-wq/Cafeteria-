namespace Cafeteria.Productos;

/// <summary>
/// Abstracción común de todos los productos de la cafetería.
/// El código cliente trabaja siempre con esta clase, nunca con las concretas (Factory Method),
/// y los complementos también son un Producto (Decorator).
/// </summary>
public abstract class Producto
{
    public abstract string Nombre { get; }
    public abstract string Descripcion { get; }

    /// <summary>Precio acumulado. En un producto sin decorar equivale al precio base.</summary>
    public abstract decimal Precio { get; }
}
