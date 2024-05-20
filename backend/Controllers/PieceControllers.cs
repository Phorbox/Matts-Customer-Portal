using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Common;
using backend.Models.Piece;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PieceController : ControllerBase
    {
        static string jsonString = System.IO.File.ReadAllText("../JSON/Piece.json");
        List<Piece> testPiece = Piece.FromJsonList(jsonString);

        [HttpGet]
        public List<Piece> Get()
        {
            // var results = new List<string>();
            // using (
            //     MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            // )
            // {
            //     connection.Open();
            //     string sql = "SELECT * FROM Piece";
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

            return testPiece;
        }

        [HttpGet("{id}")]
        public Piece Get(int id)
        {
            return testPiece.FirstOrDefault(x => x.Pieceid == id);
        }

        [HttpPost]
        public void Post([FromBody] Piece piece)
        {
            testPiece.Add(piece);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Piece piece)
        {
            var index = testPiece.FindIndex(x => x.Pieceid == id);
            testPiece[index] = piece;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = testPiece.FirstOrDefault(x => x.Pieceid == id);
            testPiece.Remove(piece);
        }
    }
}
