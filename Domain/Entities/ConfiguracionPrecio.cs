namespace OptimizadorAluminio.Domain.Entities;

public class ConfiguracionPrecio
{
    public double PrecioKg { get; set; }        // en USD o ARS
    public bool PrecioEnUSD { get; set; }
    public double CotizacionUSD { get; set; }   // USD → ARS
}
