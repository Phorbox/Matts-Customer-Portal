using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace backend.Common
{
    public static class MySQLConnection
    {
        static string password = File.ReadAllText("/run/secrets/db-password");
        static string connectionString =
            $"server=db;user=root;database=example;port=3306;password={password}";

        public static List<string> GetTitles()
        {
            var titles = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT title FROM blog";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string curString = reader.GetString(0);
                    titles.Add(curString);
                    System.Console.WriteLine(curString);

                }
                reader.Close();
                connection.Close();
            }
            // catch (Exception ex)
            // {
            //     // return Results.Problem(detail: ex.ToString());
            //     return "oops";
            // }

            return titles;
        }

        // public static void Prepare(string connectionString)
        public static void Prepare()
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();
            using var transation = connection.BeginTransaction();

            using MySqlCommand cmd1 = new MySqlCommand(
                "DROP TABLE IF EXISTS blog",
                connection,
                transation
            );
            cmd1.ExecuteNonQuery();

            using MySqlCommand cmd2 = new MySqlCommand(
                "CREATE TABLE IF NOT EXISTS blog (id int NOT NULL AUTO_INCREMENT, title varchar(255), PRIMARY KEY (id))",
                connection,
                transation
            );
            cmd2.ExecuteNonQuery();

            for (int i = 0; i < 5; i++)
            {
                using MySqlCommand insertCommand = new MySqlCommand(
                    $"INSERT INTO blog (title) VALUES ('Blog post #{i}');",
                    connection,
                    transation
                );
                insertCommand.ExecuteNonQuery();
            }
            transation.Commit();
            connection.Close();
        }
    }
}
