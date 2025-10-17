using System.Globalization;
using AgendaPro.Models;
using AgendaPro.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
CultureInfo ci = CultureInfo.InvariantCulture;
var agenda = new AgendaService();

while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("--- AgendaPro ---");
    Console.ResetColor();

    Console.WriteLine("1) Registrar persona");
    Console.WriteLine("2) Listar personas");
    Console.WriteLine("3) Crear cita");
    Console.WriteLine("4) Listar citas por PersonaId");
    Console.WriteLine("5) Mostrar todas las citas");
    Console.WriteLine("6) Salir");
    Console.Write("Elija una opción: ");
    var op = Console.ReadLine();

    try
    {
        switch (op)
        {
            case "1": RegistrarPersona(); break;
            case "2": ListarPersonas(); break;
            case "3": CrearCita(); break;
            case "4": ListarCitasPorPersona(); break;
            case "5": ListarTodasLasCitas(); break;
            case "6":
                Console.WriteLine("Saliendo del programa...");
                return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opción inválida.");
                Console.ResetColor();
                break;
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {ex.Message}");
        Console.ResetColor();
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();
}

// ---------- FUNCIONES LOCALES ----------

void RegistrarPersona()
{
    Console.WriteLine("\n[Registrar persona]");
    int id = LeerEntero("Id (entero)");
    string nombre = LeerTexto("Nombre");
    string telefono = LeerTexto("Teléfono");
    agenda.RegistrarPersona(id, nombre, telefono);
    MensajeOk("Persona registrada correctamente.");
}

void ListarPersonas()
{
    Console.WriteLine("\n[Personas]");
    var personas = agenda.ObtenerPersonas();
    if (!personas.Any()) { Console.WriteLine("(sin registros)"); return; }
    foreach (var p in personas)
        Console.WriteLine(p);
}

void CrearCita()
{
    Console.WriteLine("\n[Crear cita]");
    int personaId = LeerEntero("PersonaId");
    DateTime fecha = LeerFecha("Fecha (yyyy-MM-dd HH:mm)");
    string desc = LeerTexto("Descripción");
    agenda.CrearCita(personaId, fecha, desc);
    MensajeOk("Cita creada correctamente.");
}

void ListarCitasPorPersona()
{
    int personaId = LeerEntero("PersonaId");
    var citas = agenda.ObtenerCitasPorPersona(personaId);
    if (!citas.Any()) { Console.WriteLine("(sin citas)"); return; }
    foreach (var c in citas)
        Console.WriteLine(c);
}

void ListarTodasLasCitas()
{
    Console.WriteLine("\n[Todas las citas]");
    var citas = agenda.ObtenerTodasLasCitas();
    if (!citas.Any()) { Console.WriteLine("(sin citas)"); return; }
    foreach (var c in citas)
        Console.WriteLine(c);
}

// --------- FUNCIONES AUXILIARES ---------

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
        Console.WriteLine("Formato inválido. Ejemplo: 2025-10-16 14:30");
    }
}

void MensajeOk(string msg)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(msg);
    Console.ResetColor();
}
