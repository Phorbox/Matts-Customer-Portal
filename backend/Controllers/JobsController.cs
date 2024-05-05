using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Common;
using backend.Models.Job;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        static readonly string jsonString = System.IO.File.ReadAllText("../JSON/Job.json");
        readonly List<Job> testJob = Job.FromJson(jsonString);

        [HttpGet]
        public List<Job> Get()
        {
            Console.WriteLine(jsonString);
            // var results = new List<string>();
            // using (
            //     MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            // )
            // {
            //     connection.Open();
            //     string sql = "SELECT * FROM Job";
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

            return testJob;
        }

        [HttpGet("{id}")]
        public Job Get(int id)
        {
            return testJob.FirstOrDefault(x => x.Clientid == id);
        }

        [HttpPost]
        public void Post([FromBody] Job piece)
        {
            testJob.Add(piece);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Job piece)
        {
            var index = testJob.FindIndex(x => x.Clientid == id);
            testJob[index] = piece;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = testJob.FirstOrDefault(x => x.Clientid == id);
            testJob.Remove(piece);
        }
    }
}
