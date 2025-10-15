namespace AgendaPro.Models;

public class Cita
{
    public int PersonaId { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
