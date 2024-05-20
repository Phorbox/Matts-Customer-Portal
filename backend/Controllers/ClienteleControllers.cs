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

        [HttpGet("name/{name}")]
        public Clientele GetByName(string name)
        {
            int id = Int32.Parse(name);
            return testClientele.FirstOrDefault(x => x.Clienteleid == id);
        }
        

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
            Clientele clientele = new Clientele();
            clientele.Clienteleid = reader.GetInt64("Clienteleid");
            clientele.ClienteleName = reader.GetString("ClienteleName");
            clientele.RetentionLength = reader.GetInt64("RetentionLength");
            clientele.SlaDueDate = reader.GetInt64("SlaDueDate");
            clientele.ParentId = CommonConnection.getNullableLong(reader, "ParentId");
            return clientele;
        }
    }
}
