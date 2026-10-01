using System.Text;
using Cafeteria.Pedidos;

// Este programa solo habla con PedidoFacade: no crea productos, decoradores ni medios de pago.

Console.OutputEncoding = Encoding.UTF8;

var productos = new (string Texto, string Clave)[]
{
    ("Café", "Cafe"),
    ("Capuchino", "Capuchino"),
    ("Latte", "Latte"),
    ("Chocolate", "Chocolate")
};

var complementos = new (string Texto, string Clave)[]
{
    ("Leche", "Leche"),
    ("Crema", "Crema"),
    ("Caramelo", "Caramelo"),
    ("Azúcar", "Azucar")
};

var mediosDePago = new (string Texto, string Clave)[]
{
    ("Nequi", "Nequi"),
    ("Daviplata", "Daviplata"),
    ("Tarjeta", "Tarjeta")
};

Console.WriteLine("=== CAFETERÍA - NUEVO PEDIDO ===\n");

string producto = ElegirUno("Selecciona un producto:", productos);
List<string> extras = ElegirVarios("Selecciona complementos:", complementos);
string medioPago = ElegirUno("Selecciona el medio de pago:", mediosDePago);

var facade = new PedidoFacade();
Pedido pedido = facade.RealizarPedido(producto, extras, medioPago);

Console.WriteLine("\n===== RESULTADO DEL PEDIDO =====");
Console.WriteLine(pedido.ObtenerResumen());

static string ElegirUno(string titulo, (string Texto, string Clave)[] opciones)
{
    while (true)
    {
        Console.WriteLine(titulo);
        for (int i = 0; i < opciones.Length; i++)
            Console.WriteLine($"  {i + 1}. {opciones[i].Texto}");

        Console.Write("Opción: ");
        string? entrada = Console.ReadLine();
        if (entrada is null) Terminar();

        if (int.TryParse(entrada, out int numero) && numero >= 1 && numero <= opciones.Length)
        {
            Console.WriteLine();
            return opciones[numero - 1].Clave;
        }

        Console.WriteLine("Opción no válida, intenta de nuevo.\n");
    }
}

static List<string> ElegirVarios(string titulo, (string Texto, string Clave)[] opciones)
{
    while (true)
    {
        Console.WriteLine(titulo);
        for (int i = 0; i < opciones.Length; i++)
            Console.WriteLine($"  {i + 1}. {opciones[i].Texto}");

        Console.Write("Números separados por coma (Enter = ninguno): ");
        string? entrada = Console.ReadLine();
        if (entrada is null) Terminar();

        var seleccion = new List<string>();
        bool valido = true;

        foreach (string parte in entrada.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (int.TryParse(parte, out int numero) && numero >= 1 && numero <= opciones.Length)
            {
                seleccion.Add(opciones[numero - 1].Clave);
            }
            else
            {
                valido = false;
                break;
            }
        }

        if (valido)
        {
            Console.WriteLine();
            return seleccion;
        }

        Console.WriteLine("Selección no válida, intenta de nuevo.\n");
    }
}

[System.Diagnostics.CodeAnalysis.DoesNotReturn]
static void Terminar()
{
    Console.WriteLine("Entrada finalizada.");
    Environment.Exit(0);
}
