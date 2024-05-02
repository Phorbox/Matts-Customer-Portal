using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace backend.Common
{
    public static class CommonConnection
    {
        public static string password = File.ReadAllText("/run/secrets/db-password");
        public static string connectionString =
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
            using var transaction = connection.BeginTransaction();

            using MySqlCommand cmd1 = new MySqlCommand(
                "DROP TABLE IF EXISTS blog",
                connection,
                transaction
            );
            cmd1.ExecuteNonQuery();

            using MySqlCommand cmd2 = new MySqlCommand(
                "CREATE TABLE IF NOT EXISTS blog (id int NOT NULL AUTO_INCREMENT, title varchar(255), PRIMARY KEY (id))",
                connection,
                transaction
            );
            cmd2.ExecuteNonQuery();

            for (int i = 0; i < 5; i++)
            {
                using MySqlCommand insertCommand = new MySqlCommand(
                    $"INSERT INTO blog (title) VALUES ('Blog post #{i}');",
                    connection,
                    transaction
                );
                insertCommand.ExecuteNonQuery();
            }
            transaction.Commit();
            connection.Close();
        }

        public static void Insert(string table, List<string> stuffToInsert)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            
            using MySqlCommand cmd = new MySqlCommand(
                $"INSERT INTO ({table}) VALUES ({stuffToInsert.ToString()})",
                connection,
                transaction
            );
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static void Delete(string table, string condition)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            using MySqlCommand cmd = new MySqlCommand(
                $"DELETE FROM {table} WHERE {condition}",
                connection,
                transaction
            );
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static void Update(string table, string column, string value, string condition)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            using MySqlCommand cmd = new MySqlCommand(
                $"UPDATE {table} SET {column} = {value} WHERE {condition}",
                connection,
                transaction
            );
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static List<string> Select(string table, string column, string condition)
        {
            var results = new List<string>();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = $"SELECT {column} FROM {table} WHERE {condition}";
            using MySqlCommand cmd = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string curString = reader.GetString(0);
                results.Add(curString);
                 
            }
            reader.Close();
            connection.Close();

            return results;
        }


    }
}
