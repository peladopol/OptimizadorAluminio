using OptimizadorAluminio.Domain.Entities;
using System.Collections.Generic;

namespace OptimizadorAluminio.Domain.Services;

public class PresupuestoBuilder
{
    private readonly Presupuesto _presupuesto = new();

    public void AgregarItem(PresupuestoItem item)
    {
        _presupuesto.Items.Add(item);
    }

    public Presupuesto Build()
    {
        return _presupuesto;
    }
}
