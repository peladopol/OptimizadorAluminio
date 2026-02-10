using Microsoft.Data.Sqlite;
using OptimizadorAluminio.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OptimizadorAluminio.Data.SQLite;

public class PerfilRepository
{
    public void Insert(Perfil perfil)
    {
        using var conn = Database.GetConnection();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Perfil 
            (Codigo, Descripcion, PesoKgMetro, LargoBarraMM, LargoUtilMM, CorteExtremoMM)
            VALUES 
            (@Codigo, @Descripcion, @Peso, @Barra, @Util, @Corte)";

        cmd.Parameters.AddWithValue("@Codigo", perfil.Codigo);
        cmd.Parameters.AddWithValue("@Descripcion", perfil.Descripcion);
        cmd.Parameters.AddWithValue("@Peso", perfil.PesoKgMetro);
        cmd.Parameters.AddWithValue("@Barra", perfil.LargoBarraMM);
        cmd.Parameters.AddWithValue("@Util", perfil.LargoUtilMM);
        cmd.Parameters.AddWithValue("@Corte", perfil.CorteExtremoMM);

        cmd.ExecuteNonQuery();
    }

    public List<Perfil> GetAll()
    {
        var list = new List<Perfil>();

        using var conn = Database.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Perfil";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Perfil
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                PesoKgMetro = reader.GetDouble(reader.GetOrdinal("PesoKgMetro")),
                LargoBarraMM = reader.GetInt32(reader.GetOrdinal("LargoBarraMM")),
                LargoUtilMM = reader.GetInt32(reader.GetOrdinal("LargoUtilMM")),
                CorteExtremoMM = reader.GetInt32(reader.GetOrdinal("CorteExtremoMM"))
            });
        }

        return list;
    }
}
