namespace AgendaPro.Models;

public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }

    public Persona() { }

    public Persona(int id, string nombre, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
    }

    public override string ToString() => $"{Id,3} | {Nombre,-15} | {Telefono,-10}";
}