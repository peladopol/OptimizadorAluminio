namespace OptimizadorAluminio.Domain.Entities;

public class Tramo
{
    public string CodigoPerfil { get; set; } = string.Empty;

    public int LargoMM { get; set; }

    public CorteAngulo AnguloIzquierdo { get; set; }

    public CorteAngulo AnguloDerecho { get; set; }

    /// <summary>
    /// 1 mm por cada extremo
    /// </summary>
    public int PerdidaPorCorteMM => 2;
}
