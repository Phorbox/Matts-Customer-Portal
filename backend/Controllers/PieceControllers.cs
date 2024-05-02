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
        List<Piece> testPiece = Piece.FromJson(
            @"[
  {
    'pieceid': '1',
    'workorderid': '1',
    'clientid': '1',
    'Status': 'Approved',
    'Batch Name': 'batch1',
    'Pages': '1',
    'Simplex': '',
    'sequence': '1',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '2',
    'workorderid': '1',
    'clientid': '1',
    'Status': 'Approved',
    'Batch Name': 'batch1',
    'Pages': '1',
    'Simplex': '',
    'sequence': '2',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '3',
    'workorderid': '1',
    'clientid': '1',
    'Status': 'Approved',
    'Batch Name': 'batch1',
    'Pages': '1',
    'Simplex': '',
    'sequence': '3',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '4',
    'workorderid': '1',
    'clientid': '1',
    'Status': 'Approved',
    'Batch Name': 'batch2',
    'Pages': '2',
    'Simplex': '',
    'sequence': '1',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '5',
    'workorderid': '1',
    'clientid': '1',
    'Status': 'Approved',
    'Batch Name': 'batch2',
    'Pages': '2',
    'Simplex': '',
    'sequence': '2',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '6',
    'workorderid': '2',
    'clientid': '2',
    'Status': 'Rejected',
    'Batch Name': 'pulls',
    'Pages': '1',
    'Simplex': 'TRUE',
    'sequence': '1',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '7',
    'workorderid': '2',
    'clientid': '2',
    'Status': 'Approved',
    'Batch Name': 'batchf',
    'Pages': '6',
    'Simplex': 'TRUE',
    'sequence': '1',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '8',
    'workorderid': '2',
    'clientid': '2',
    'Status': 'Approved',
    'Batch Name': 'batch1',
    'Pages': '1',
    'Simplex': '',
    'sequence': '2',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '9',
    'workorderid': '2',
    'clientid': '2',
    'Status': 'Approved',
    'Batch Name': 'batch2',
    'Pages': '2',
    'Simplex': '',
    'sequence': '1',
    'Retention Start Day': '4/30/24'
  },
  {
    'pieceid': '10',
    'workorderid': '2',
    'clientid': '2',
    'Status': 'Approved',
    'Batch Name': 'batch2',
    'Pages': '2',
    'Simplex': '',
    'sequence': '2',
    'Retention Start Day': '4/30/24'
  }
]"
        );

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
