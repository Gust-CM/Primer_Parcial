using System.Globalization;
using ElectroPlus.Models;

List<Producto> productos = new(); // Lista para almacenar productos
CultureInfo ci = CultureInfo.InvariantCulture;

while (true) // Bucle principal del programa
{
    Console.WriteLine("\n--- Inventario ElectroPlus ---");
    Console.WriteLine("1) Agregar producto");
    Console.WriteLine("2) Listar productos");
    Console.WriteLine("3) Buscar por Código");
    Console.WriteLine("4) Mostrar agotados (Cantidad = 0)");
    Console.WriteLine("5) Salir");
    Console.Write("Elija una opción: ");
    var op = Console.ReadLine();

    switch (op)
    {
        case "1": Agregar(); break;
        case "2": Listar(); break;
        case "3": Buscar(); break;
        case "4": ListarAgotados(); break;
        case "5": return;
        default: Console.WriteLine("Opción inválida."); break;
    }
}

void Agregar()
{
    Console.WriteLine("\n[Agregar producto]");
    string codigo = LeerTexto("Código");

    if (productos.Any(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("Ya existe un producto con ese Código.");
        return;
    }

    string nombre = LeerTexto("Nombre");
    decimal precio = LeerDecimal("Precio (decimal)");
    int cantidad = LeerEntero("Cantidad (entera)");

    productos.Add(new Producto { Codigo = codigo, Nombre = nombre, Precio = precio, Cantidad = cantidad });
    Console.WriteLine("Producto agregado correctamente.");
}

void Listar()
{
    Console.WriteLine("\n[Listado]");
    if (!productos.Any()) { Console.WriteLine("(sin productos)"); return; }
    foreach (var p in productos)
        Console.WriteLine($"{p.Codigo}|{p.Nombre}|{p.Precio.ToString("0.00", ci)}|{p.Cantidad}");
}

void Buscar()
{
    Console.Write("\nIngrese Código a buscar: ");
    string? codigo = Console.ReadLine();
    var p = productos.FirstOrDefault(x => x.Codigo.Equals(codigo ?? string.Empty, StringComparison.OrdinalIgnoreCase));
    if (p == null) Console.WriteLine("No encontrado.");
    else Console.WriteLine($"{p.Codigo}|{p.Nombre}|{p.Precio.ToString("0.00", ci)}|{p.Cantidad}");
}

void ListarAgotados()
{
    Console.WriteLine("\n[Agotados]");
    var q = productos.Where(p => p.Cantidad == 0);
    if (!q.Any()) { Console.WriteLine("(ninguno)"); return; }
    foreach (var p in q)
        Console.WriteLine($"{p.Codigo}|{p.Nombre}|{p.Precio.ToString("0.00", ci)}|{p.Cantidad}");
}

string LeerTexto(string etiqueta)
{
    while (true)
    {
        Console.Write($"{etiqueta}: ");
        string? s = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
        Console.WriteLine("Entrada vacía. Intente de nuevo.");
    }
}

decimal LeerDecimal(string etiqueta)
{
    while (true)
    {
        Console.Write($"{etiqueta}: ");
        string? s = Console.ReadLine();
        if (decimal.TryParse(s, NumberStyles.Number, ci, out var val)) return val;
        Console.WriteLine("Formato inválido. Use números con punto decimal (ej. 123.45).");
    }
}

int LeerEntero(string etiqueta)
{
    while (true)
    {
        Console.Write($"{etiqueta}: ");
        string? s = Console.ReadLine();
        if (int.TryParse(s, out var v)) return v;
        Console.WriteLine("Debe ser un número entero.");
    }
}
