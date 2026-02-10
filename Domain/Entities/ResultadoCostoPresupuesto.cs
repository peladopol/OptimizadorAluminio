using System.Collections.Generic;
using System.Linq;

namespace OptimizadorAluminio.Domain.Entities;

public class ResultadoCostoPresupuesto
{
    public List<ResultadoCostoPerfil> Detalle { get; set; } = new();

    public double KgTotal => Detalle.Sum(d => d.KgTotal);
    public double CostoTotal => Detalle.Sum(d => d.CostoTotal);
}
