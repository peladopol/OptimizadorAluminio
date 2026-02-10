using Microsoft.Data.Sqlite;
using System.IO;

namespace OptimizadorAluminio.Data.SQLite
{
    public static class Database
    {
        private static readonly string DbPath = "app.db";

        public static SqliteConnection GetConnection()
        {
            if (!File.Exists(DbPath))
            {
                using var conn = new SqliteConnection($"Data Source={DbPath}");
                conn.Open();
            }

            return new SqliteConnection($"Data Source={DbPath}");
        }
    }
}
