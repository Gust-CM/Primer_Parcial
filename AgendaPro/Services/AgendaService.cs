using System.Text.Json;
using AgendaPro.Models;

namespace AgendaPro.Services;

public class AgendaService
{
    private readonly string personasPath = "personas.json";
    private readonly string citasPath = "citas.json";

    private readonly List<Persona> _personas = new();
    private readonly List<Cita> _citas = new();

    public AgendaService()
    {
        CargarDatos();
    }

    // Registrar nueva persona
    public void RegistrarPersona(int id, string nombre, string telefono)
    {
        if (_personas.Any(p => p.Id == id))
            throw new InvalidOperationException("Ya existe una persona con ese Id.");
        _personas.Add(new Persona(id, nombre, telefono));
        GuardarDatos();
    }

    // Listar todas las personas
    public IEnumerable<Persona> ObtenerPersonas() => _personas.OrderBy(p => p.Id);

    // Crear cita
    public void CrearCita(int personaId, DateTime fecha, string descripcion)
    {
        if (!_personas.Any(p => p.Id == personaId))
            throw new InvalidOperationException("Persona no encontrada.");
        _citas.Add(new Cita { PersonaId = personaId, Fecha = fecha, Descripcion = descripcion });
        GuardarDatos();
    }

    // Listar citas por persona
    public IEnumerable<Cita> ObtenerCitasPorPersona(int personaId) =>
        _citas.Where(c => c.PersonaId == personaId).OrderBy(c => c.Fecha);

    // Listar todas las citas
    public IEnumerable<Cita> ObtenerTodasLasCitas() => _citas.OrderBy(c => c.Fecha);

    // Guardar y cargar datos
    private void GuardarDatos()
    {
        File.WriteAllText(personasPath, JsonSerializer.Serialize(_personas, new JsonSerializerOptions { WriteIndented = true }));
        File.WriteAllText(citasPath, JsonSerializer.Serialize(_citas, new JsonSerializerOptions { WriteIndented = true }));
    }

    private void CargarDatos()
    {
        if (File.Exists(personasPath))
        {
            var json = File.ReadAllText(personasPath);
            _personas.AddRange(JsonSerializer.Deserialize<List<Persona>>(json) ?? []);
        }

        if (File.Exists(citasPath))
        {
            var json = File.ReadAllText(citasPath);
            _citas.AddRange(JsonSerializer.Deserialize<List<Cita>>(json) ?? []);
        }
    }
}
