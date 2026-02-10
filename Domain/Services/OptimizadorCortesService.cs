using OptimizadorAluminio.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OptimizadorAluminio.Domain.Services;

public class OptimizadorCortesService
{
    public List<BarraCorte> Optimizar(List<Tramo> tramos)
    {
        var barras = new List<BarraCorte>();

        // Estrategia: First Fit Decreasing
        var ordenados = tramos
            .OrderByDescending(t => t.LargoMM)
            .ToList();

        foreach (var tramo in ordenados)
        {
            bool colocado = false;

            foreach (var barra in barras)
            {
                if (barra.PuedeAgregar(tramo))
                {
                    barra.Agregar(tramo);
                    colocado = true;
                    break;
                }
            }

            if (!colocado)
            {
                var nueva = new BarraCorte();
                nueva.Agregar(tramo);
                barras.Add(nueva);
            }
        }

        return barras;
    }
}
