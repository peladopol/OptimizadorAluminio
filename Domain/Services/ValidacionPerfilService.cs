using OptimizadorAluminio.Domain.Entities;

namespace OptimizadorAluminio.Domain.Services;

public class ValidacionPerfilService
{
    public void Validar(Perfil perfil)
    {
        if (string.IsNullOrWhiteSpace(perfil.Codigo))
            throw new ArgumentException("El código del perfil es obligatorio");

        if (perfil.LargoUtilMM <= 0)
            throw new ArgumentException("El largo util debe ser mayor a cero");
    }
}
