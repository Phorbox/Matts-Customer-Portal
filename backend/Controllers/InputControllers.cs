using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Common;
using backend.Models.Input;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputController : ControllerBase
    {
        static readonly string jsonString = System.IO.File.ReadAllText("../JSON/Input.json");
        readonly List<Input> testInput = Input.FromJsonList(jsonString);

        [HttpGet]
        public List<Input> Get()
        {
            var result = new List<Input>();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql =
                    @"  
                        SELECT *
                        FROM Input;";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ConvertReaderToInput(reader));
                }
                reader.Close();
                connection.Close();
            }

            return result;
        }

        [HttpGet("{id}")]
        public Input Get(int id)
        {
            var result = new Input();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql =
                    $@"  
                        SELECT *
                        FROM Input
                        join Job on Input.Jobid = Job.Jobid
                        WHERE Inputid = {id};";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                result = ConvertReaderToInput(reader);
                reader.Close();
                connection.Close();
            }
            return result;
        }

        [HttpGet("name/{name}")]
        public Input GetByName(string name)
        {
            int id = Int32.Parse(name);
            return testInput.FirstOrDefault(x => x.Inputid == id);
        }

        [HttpPost]
        public Input Post([FromBody] Input input)
        {
            // var piece = Input.FromJson(input);
            var piece = input;
            var result = new Input();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                // string sql =
                //     $@"  
                //         INSERT INTO Input (Filename, InputUri)
                //         VALUES ('{piece.Filename}', '{piece.InputUri});

                //         SELECT *
                //         FROM Input
                //         WHERE Inputid = LAST_INSERT_ID();
                //         ";
                string sql =
                    $@"  
                        INSERT INTO Input (Filename)
                        VALUES ('{piece.Filename}');

                        SELECT *
                        FROM Input
                        WHERE Inputid = LAST_INSERT_ID();
                        ";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                result = ConvertReaderToInput(reader);

                reader.Close();
                connection.Close();
            }
            return result;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Input piece)
        {
            var index = testInput.FindIndex(x => x.Inputid == id);
            testInput[index] = piece;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = testInput.FirstOrDefault(x => x.Inputid == id);
            testInput.Remove(piece);
        }

        private static Input ConvertReaderToInput(MySqlDataReader reader)
        {
            Input input = new Input();
            input.Inputid = reader.GetInt64("Inputid");
            input.Filename = reader.GetString("Filename");
            input.Jobid = CommonConnection.getNullableLong(reader,"Jobid");
            input.StoragePriority = reader.GetString("StoragePriority");
            input.InputUri = CommonConnection.getNullableString(reader,"InputUri");
            return input;
        }
    }
}
