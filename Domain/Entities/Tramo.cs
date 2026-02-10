namespace OptimizadorAluminio.Domain.Entities;

public class Tramo
{
    public int LargoMM { get; set; }

    public CorteAngulo AnguloIzquierdo { get; set; }
    public CorteAngulo AnguloDerecho { get; set; }

    public int PerdidaPorCorteMM => 2; // 1 mm por lado
}
