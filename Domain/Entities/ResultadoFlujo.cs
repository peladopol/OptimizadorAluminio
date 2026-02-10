namespace OptimizadorAluminio.Domain.Entities;

public class ResultadoFlujo
{
    public Presupuesto Presupuesto { get; set; } = null!;
    public Dictionary<string, List<BarraOptimizada>> BarrasPorPerfil { get; set; } = new();
    public ResultadoCostoPresupuesto Costos { get; set; } = new();
}
