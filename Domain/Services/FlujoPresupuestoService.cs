using System.Collections.Generic;
using System.Linq;
using OptimizadorAluminio.Infrastructure.Import;
using OptimizadorAluminio.Data.SQLite;
using OptimizadorAluminio.Domain.DTOs;
using OptimizadorAluminio.Domain.Entities;

namespace OptimizadorAluminio.Domain.Services;

public class FlujoPresupuestoService
{
    // Infraestructura
    private readonly ExcelImportService _excel;
    private readonly DesperdicioRepository _desperdicio;
    private readonly PerfilRepository _perfilRepo;
    private readonly ConfiguracionPrecioRepository _precioRepo;

    // Dominio
    private readonly OptimizadorCortesService _optimizador;
    private readonly CalculoCostosService _costos;

    public FlujoPresupuestoService()
    {
        _excel = new ExcelImportService();
        _desperdicio = new DesperdicioRepository();
        _perfilRepo = new PerfilRepository();
        _precioRepo = new ConfiguracionPrecioRepository();

        _optimizador = new OptimizadorCortesService();
        _costos = new CalculoCostosService();
    }

    /// <summary>
    /// Ejecuta la optimización de cortes a partir de una lista de tramos
    /// </summary>
    public ResultadoOptimizacion OptimizarCortes(List<Tramo> tramos)
    {
        var barras = _optimizador.Optimizar(tramos);

        var sobranteTotal = barras.Sum(b => b.SobranteMM);

        _desperdicio.Registrar(sobranteTotal);

        return new ResultadoOptimizacion
        {
            Barras = barras,
            SobranteTotalMM = sobranteTotal
        };
    }

    /// <summary>
    /// Importa un Excel y ejecuta directamente la optimización
    /// </summary>
    public ResultadoOptimizacion OptimizarDesdeExcel(string pathExcel)
    {
        var tramos = _excel.ImportarTramos(pathExcel);
        return OptimizarCortes(tramos);
    }

    /// <summary>
    /// Calcula los costos del presupuesto en base a las barras optimizadas
    /// </summary>
    public ResultadoCostoPresupuesto CalcularCostos(
        Dictionary<string, List<BarraCorte>> barrasPorPerfil)
    {
        var perfiles = _perfilRepo.GetAll();
        var configuracion = _precioRepo.GetActual();

        return _costos.Calcular(barrasPorPerfil, perfiles, configuracion);
    }
}
