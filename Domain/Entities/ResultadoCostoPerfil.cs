namespace OptimizadorAluminio.Domain.Entities;

public class ResultadoCostoPerfil
{
    public string CodigoPerfil { get; set; } = "";
    public int CantidadBarras { get; set; }
    public double KgPorBarra { get; set; }
    public double KgTotal => CantidadBarras * KgPorBarra;
    public double CostoTotal { get; set; }
}
