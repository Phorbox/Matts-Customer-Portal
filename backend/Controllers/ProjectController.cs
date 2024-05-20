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
        readonly List<Project> testProject = Project.FromJsonList(jsonString);

        [HttpGet]
        public List<Project> Get()
        {
            var result = new List<Project>();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql =
                    @"  
                        SELECT *
                        FROM Project
                        Join Clientele ON Project.Clienteleid = Clientele.Clienteleid;";

                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ConvertReaderToProject(reader));
                }
                reader.Close();
                connection.Close();
            }

            return result;
        }

        [HttpGet("{id}")]
        public Project Get(int id)
        {
            return testProject.FirstOrDefault(x => x.Clienteleid == id);
        }

        [HttpPost]
        public void Post([FromBody] Project piece)
        {
            testProject.Add(piece);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Project piece)
        {
            var index = testProject.FindIndex(x => x.Clienteleid == id);
            testProject[index] = piece;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = testProject.FirstOrDefault(x => x.Clienteleid == id);
            testProject.Remove(piece);
        }

        private static Project ConvertReaderToProject(MySqlDataReader reader)
        {
            Project project = new Project();
            project.Projectid = reader.GetInt64("Projectid");
            project.ProjectName = reader.GetString("ProjectName");
            project.Clienteleid = reader.GetInt64("Clienteleid");
            project.SlaOveride = CommonConnection.getNullableLong(reader, "SlaOveride");
            project.Approval = CommonConnection.getNullableString(reader, "Approval");
            project.ClienteleName = reader.GetString("ClienteleName");
            project.RetentionLength = reader.GetInt64("RetentionLength");
            project.SlaDueDate = reader.GetInt64("SlaDueDate");
            project.ParentId = CommonConnection.getNullableLong(reader, "ParentId");

            return project;
        }
    }
}
