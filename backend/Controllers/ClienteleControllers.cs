using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Common;
using backend.Models.Clientele;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteleController : ControllerBase
    {
        static string jsonString = System.IO.File.ReadAllText("../JSON/Clientele.json");
        List<Clientele> testClientele = Clientele.FromJson(jsonString);

        [HttpGet]
        public List<Clientele> Get()
        {
            Console.WriteLine(jsonString);
            // var results = new List<string>();
            // using (
            //     MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            // )
            // {
            //     connection.Open();
            //     string sql = "SELECT * FROM Clientele";
            //     using MySqlCommand cmd = new MySqlCommand(sql, connection);
            //     using MySqlDataReader reader = cmd.ExecuteReader();
            //     while (reader.Read())
            //     {
            //         string curString = reader.GetString(0);
            //         results.Add(curString);
            //     }
            //     reader.Close();
            //     connection.Close();
            // }

            return testClientele;
        }

        [HttpGet("{id}")]
        public Clientele Get(int id)
        {
            return testClientele.FirstOrDefault(x => x.Clientid == id);
        }

        [HttpPost]
        public void Post([FromBody] Clientele piece)
        {
            testClientele.Add(piece);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Clientele piece)
        {
            var index = testClientele.FindIndex(x => x.Clientid == id);
            testClientele[index] = piece;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = testClientele.FirstOrDefault(x => x.Clientid == id);
            testClientele.Remove(piece);
        }
    }
}
