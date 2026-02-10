using System.Collections.Generic;
using System.Linq;

namespace OptimizadorAluminio.Domain.Entities;

public class BarraCorte
{
    public int LargoUtilMM { get; } = 6100;

    public List<Tramo> Tramos { get; } = new();

    public int LargoUsadoMM =>
        Tramos.Sum(t => t.LargoMM + t.PerdidaPorCorteMM);

    public int SobranteMM => LargoUtilMM - LargoUsadoMM;

    public bool PuedeAgregar(Tramo tramo)
    {
        return LargoUsadoMM + tramo.LargoMM + tramo.PerdidaPorCorteMM <= LargoUtilMM;
    }

    public void Agregar(Tramo tramo)
    {
        Tramos.Add(tramo);
    }
}
