namespace OptimizadorAluminio.Domain.Entities;

public class Perfil
{
    public int Id { get; set; }
    public string Codigo { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public double PesoKgMetro { get; set; }
    public int LargoBarraMM { get; set; } = 6150;
    public int LargoUtilMM { get; set; } = 6100;
    public int CorteExtremoMM { get; set; } = 1;
}
