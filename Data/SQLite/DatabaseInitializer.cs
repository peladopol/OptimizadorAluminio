using Microsoft.Data.Sqlite;
using System.IO;

namespace OptimizadorAluminio.Data.SQLite;

public static class DatabaseInitializer
{
    public static void Initialize()
    {
        // Abrir conexión (crea el archivo si no existe)
        using var conn = Database.GetConnection();

        // Verificar si ya existe la tabla Perfil (tabla base)
        using var checkCmd = conn.CreateCommand();
        checkCmd.CommandText =
            "SELECT name FROM sqlite_master WHERE type='table' AND name='Perfil'";

        var exists = checkCmd.ExecuteScalar();

        if (exists != null)
        {
            // La base ya está inicializada
            return;
        }

        // Leer schema.sql
        var schemaSql = File.ReadAllText("schema.sql");

        using var schemaCmd = conn.CreateCommand();
        schemaCmd.CommandText = schemaSql;
        schemaCmd.ExecuteNonQuery();
    }
}
