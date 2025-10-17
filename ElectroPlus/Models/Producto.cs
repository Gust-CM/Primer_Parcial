namespace ElectroPlus.Models;

public class Producto //Clase pública para representar un producto en el inventario
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
}
