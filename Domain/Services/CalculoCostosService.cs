using OptimizadorAluminio.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OptimizadorAluminio.Domain.Services;

public class CalculoCostosService
{
    public ResultadoCostoPresupuesto Calcular(
        Dictionary<string, List<BarraCorte>> barrasPorPerfil,
        List<Perfil> perfiles,
        ConfiguracionPrecio config)
    {
        var resultado = new ResultadoCostoPresupuesto();

        foreach (var kvp in barrasPorPerfil)
        {
            var codigo = kvp.Key;
            var cantidadBarras = kvp.Value.Count;

            var perfil = perfiles.First(p => p.Codigo == codigo);

            // kg por barra facturable (6,15 m)
            double kgPorBarra = perfil.PesoKgMetro * 6.15;

            double precioKgFinal =
                config.PrecioEnUSD
                    ? config.PrecioKg * config.CotizacionUSD
                    : config.PrecioKg;

            double costoTotal =
                kgPorBarra * cantidadBarras * precioKgFinal;

            resultado.Detalle.Add(new ResultadoCostoPerfil
            {
                CodigoPerfil = codigo,
                CantidadBarras = cantidadBarras,
                KgPorBarra = kgPorBarra,
                CostoTotal = costoTotal
            });
        }

        return resultado;
    }
}
