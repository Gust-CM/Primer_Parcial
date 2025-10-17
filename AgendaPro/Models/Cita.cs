namespace AgendaPro.Models;

public class Cita //Clase pública para representar una cita en la agenda
{
    public int PersonaId { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
