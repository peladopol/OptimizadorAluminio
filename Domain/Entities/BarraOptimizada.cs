namespace OptimizadorAluminio.Domain.Entities;

public class BarraOptimizada
{
    public string CodigoPerfil { get; set; } = "";
    public int LargoUtilMM { get; set; }
    public List<CorteAsignado> Cortes { get; set; } = new();

    public int LargoUsado =>
        Cortes.Sum(c => c.LargoMM) + Cortes.Count; // 1 mm por corte

    public int SobranteMM =>
        LargoUtilMM - LargoUsado;
}
