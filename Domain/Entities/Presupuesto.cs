using System;
using System.Collections.Generic;

namespace OptimizadorAluminio.Domain.Entities;

public class Presupuesto
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public string Descripcion { get; set; } = string.Empty;

    // 🔴 ESTO ES LO QUE FALTABA
    public List<PresupuestoItem> Items { get; set; } = new();

    public double CostoOriginal { get; set; }
}
