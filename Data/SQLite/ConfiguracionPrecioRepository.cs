using OptimizadorAluminio.Domain.Entities;

namespace OptimizadorAluminio.Data.SQLite;

public class ConfiguracionPrecioRepository
{
    public ConfiguracionPrecio GetActual()
    {
        return new ConfiguracionPrecio();
    }
}
