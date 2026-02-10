namespace OptimizadorAluminio.Domain.Entities;

public class PresupuestoItem
{
    public int Id { get; set; }
    public int PresupuestoId { get; set; }
    public string CodigoPerfil { get; set; } = "";
    public int LargoMM { get; set; }
    public string Ventana { get; set; } = "";
    public int Angulo { get; set; } // 45 o 90
}
