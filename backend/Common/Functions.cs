using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace backend.Common
{
    public static class CommonConnection
    {
        // public static string password = File.ReadAllText("/run/secrets/db-password");
        public static string password = "db-q5n2g";
        public static string connectionString =
            $"server=db;user=root;database=main;port=3306;password={password}";

        // public static string server = "outputsolutionsportaldb";
        // public static string user = "mattsmakingademo";
        // public static string database = "main";
        // public static string port = "3306";
        // public static string password = "oNVYAXFp1JsRzDpHin0U";
        // public static string connectionString =
        //     $"server={server};user={user};database={database};port={port};password={password}";
        public static DateTime? getNullableDate(MySqlDataReader reader, string columnName)
        {
            return Convert.IsDBNull(reader.GetValue(reader.GetOrdinal(columnName)))
                ? null
                : reader.GetDateTime(columnName);
        }

        public static string? getNullableString(MySqlDataReader reader, string columnName)
        {
            return Convert.IsDBNull(reader.GetValue(reader.GetOrdinal(columnName)))
                ? null
                : reader.GetString(columnName);
        }

        public static long? getNullableLong(MySqlDataReader reader, string columnName)
        {
            return Convert.IsDBNull(reader.GetValue(reader.GetOrdinal(columnName)))
                ? null
                : reader.GetInt64(columnName);
        }
    }
}
