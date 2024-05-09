using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Common;
using backend.Models.Project;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        static readonly string jsonString = System.IO.File.ReadAllText("../JSON/Project.json");
        readonly List<Project> testProject = Project.FromJson(jsonString);

        [HttpGet]
        public List<Project> Get()
        {
            Console.WriteLine(jsonString);
            // var results = new List<string>();
            // using (
            //     MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            // )
            // {
            //     connection.Open();
            //     string sql = "SELECT * FROM Project";
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

            return testProject;
        }

        [HttpGet("{id}")]
        public Project Get(int id)
        {
            return testProject.FirstOrDefault(x => x.Clientid == id);
        }

        [HttpPost]
        public void Post([FromBody] Project piece)
        {
            testProject.Add(piece);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Project piece)
        {
            var index = testProject.FindIndex(x => x.Clientid == id);
            testProject[index] = piece;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = testProject.FirstOrDefault(x => x.Clientid == id);
            testProject.Remove(piece);
        }
    }
}
