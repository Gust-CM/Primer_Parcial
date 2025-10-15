using System.Globalization;
using AgendaPro.Models;

List<Persona> personas = new();
List<Cita> citas = new();
CultureInfo ci = CultureInfo.InvariantCulture;

while (true)
{
    Console.WriteLine("\n--- AgendaPro ---");
    Console.WriteLine("1) Registrar persona");
    Console.WriteLine("2) Listar personas");
    Console.WriteLine("3) Crear cita");
    Console.WriteLine("4) Listar citas por PersonaId");
    Console.WriteLine("5) Mostrar todas las citas");
    Console.WriteLine("6) Salir");
    Console.Write("Elija una opción: ");
    var op = Console.ReadLine();

    switch (op)
    {
        case "1": RegistrarPersona(); break;
        case "2": ListarPersonas(); break;
        case "3": CrearCita(); break;
        case "4": ListarCitasPorPersona(); break;
        case "5": ListarTodasLasCitas(); break;
        case "6": return;
        default: Console.WriteLine("Opción inválida."); break;
    }
}

void RegistrarPersona()
{
    Console.WriteLine("\n[Registrar persona]");
    int id = LeerEntero("Id (entero)");
    if (personas.Any(p => p.Id == id))
    {
        Console.WriteLine("Ya existe una persona con ese Id.");
        return;
    }
    string nombre = LeerTexto("Nombre");
    string telefono = LeerTexto("Teléfono");
    personas.Add(new Persona(id, nombre, telefono));
    Console.WriteLine("Persona registrada.");
}

void ListarPersonas()
{
    Console.WriteLine("\n[Personas]");
    if (!personas.Any()) { Console.WriteLine("(sin registros)"); return; }
    foreach (var p in personas)
        Console.WriteLine($"{p.Id}|{p.Nombre}|{p.Telefono}");
}

void CrearCita()
{
    Console.WriteLine("\n[Crear cita]");
    int personaId = LeerEntero("PersonaId");
    var persona = personas.FirstOrDefault(p => p.Id == personaId);
    if (persona is null)
    {
        Console.WriteLine("No existe una persona con ese Id.");
        return;
    }

    DateTime fecha = LeerFecha("Fecha (yyyy-MM-dd HH:mm)");
    string desc = LeerTexto("Descripción");

    citas.Add(new Cita { PersonaId = personaId, Fecha = fecha, Descripcion = desc });
    Console.WriteLine("Cita creada.");
}

void ListarCitasPorPersona()
{
    int personaId = LeerEntero("PersonaId");
    var q = citas.Where(c => c.PersonaId == personaId).OrderBy(c => c.Fecha);
    if (!q.Any()) { Console.WriteLine("(sin citas)"); return; }
    foreach (var c in q)
        Console.WriteLine($"{c.PersonaId}|{c.Fecha:yyyy-MM-dd HH:mm}|{c.Descripcion}");
}

void ListarTodasLasCitas()
{
    Console.WriteLine("\n[Todas las citas]");
    if (!citas.Any()) { Console.WriteLine("(sin citas)"); return; }
    foreach (var c in citas.OrderBy(c => c.Fecha))
        Console.WriteLine($"{c.PersonaId}|{c.Fecha:yyyy-MM-dd HH:mm}|{c.Descripcion}");
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

DateTime LeerFecha(string etiqueta)
{
    while (true)
    {
        Console.Write($"{etiqueta}: ");
        string? s = Console.ReadLine();
        if (DateTime.TryParseExact(s, "yyyy-MM-dd HH:mm", ci, DateTimeStyles.None, out var dt)) return dt;
        Console.WriteLine("Formato inválido. Ejemplo: 2025-10-14 14:30");
    }
}
