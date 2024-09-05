using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Common;
using backend.Models.Clientele;
using backend.Models.Job;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteleController : ControllerBase
    {
        static readonly string jsonString = System.IO.File.ReadAllText("../JSON/Clientele.json");
        readonly List<Clientele> testClientele = Clientele.FromJsonList(jsonString);

        [HttpGet]
        public List<Clientele> Get()
        {
            var result = new List<Clientele>();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql =
                    @"  
                        SELECT *
                        FROM Clientele;";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ConvertReaderToClientele(reader));
                }
                reader.Close();
                connection.Close();
            }

            return result ;
        }
      
        [HttpGet("{id}")]
        public Clientele Get(int id)
        {
            return testClientele.FirstOrDefault(x => x.Clienteleid == id);
        }

        // [HttpGet("name/{name}")]
        // public Clientele GetByName(string name)
        // {
      
        //     return testClientele.FirstOrDefault(x => x.ClienteleName == name);
        // }
        

        // [HttpPost]
        // public void Post([FromBody] String body)
        // {
        //     Clientele clientele = Clientele.FromJson(body);
        // }
        
        [HttpPost]
        public void Post([FromBody] Clientele piece)
        {
            testClientele.Add(piece);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Clientele piece)
        {
            var index = testClientele.FindIndex(x => x.Clienteleid == id);
            testClientele[index] = piece;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = testClientele.FirstOrDefault(x => x.Clienteleid == id);
            testClientele.Remove(piece);
        }

        private static Clientele ConvertReaderToClientele(MySqlDataReader reader)
        {
            Clientele clientele = new Clientele
            {
                Clienteleid = reader.GetInt64("Clienteleid"),
                ClienteleName = reader.GetString("ClienteleName"),
                RetentionLength = reader.GetInt64("RetentionLength"),
                SlaDueDate = reader.GetInt64("SlaDueDate"),
                ParentId = CommonConnection.getNullableLong(reader, "ParentId")
            };
            return clientele;
        }
    }
}
