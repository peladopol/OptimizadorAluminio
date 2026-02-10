using System.Collections.Generic;
using OptimizadorAluminio.Domain.Entities;

namespace OptimizadorAluminio.Domain.DTOs;

public class ResultadoOptimizacion
{
    public List<BarraCorte> Barras { get; set; } = new();
    public int SobranteTotalMM { get; set; }
}
