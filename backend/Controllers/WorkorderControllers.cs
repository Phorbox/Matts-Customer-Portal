using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Common;
using backend.Models.Workorder;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkorderController : ControllerBase
    {
        List<Workorder> testWorkorder = new List<Workorder>
        {
            new Workorder
            {
                Workorderid = 1,
                Jobid = 1,
                Clientid = 1,
                Inputid = 1,
                Status = "Processing",
                DateApproved = new System.DateTime(2020, 1, 1).ToString(),
                DueDate = new System.DateTime(2021, 1, 1).ToString(),
                DateCreated = new System.DateTime(2022, 1, 1).ToString()
            },
            new Workorder
            {
                Workorderid = 2,
                Jobid = 2,
                Clientid = 1,
                Inputid = 2,
                Status = "Pending Approval",
                DateApproved = new System.DateTime(2020, 1, 1).ToString(),
                DueDate = new System.DateTime(2021, 1, 1).ToString(),
                DateCreated = new System.DateTime(2022, 1, 1).ToString()
            },
            new Workorder
            {
                Workorderid = 3,
                Jobid = 1,
                Clientid = 2,
                Inputid = 3,
                Status = "Processing",
                DateApproved = new System.DateTime(2020, 1, 1).ToString(),
                DueDate = new System.DateTime(2021, 1, 1).ToString(),
                DateCreated = new System.DateTime(2022, 1, 1).ToString()
            }
        };

        [HttpGet]
        public List<Workorder> Get()
        {
            // var results = new List<string>();
            // using (
            //     MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            // )
            // {
            //     connection.Open();
            //     string sql = "SELECT * FROM Workorder";
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

            return testWorkorder;
        }

        [HttpGet("{id}")]
        public Workorder Get(int id)
        {
            return testWorkorder.FirstOrDefault(x => x.Workorderid == id);
        }

        [HttpPost]
        public void Post([FromBody] Workorder workorder)
        {
            testWorkorder.Add(workorder);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Workorder workorder)
        {
            var index = testWorkorder.FindIndex(x => x.Workorderid == id);
            testWorkorder[index] = workorder;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var workorder = testWorkorder.FirstOrDefault(x => x.Workorderid == id);
            testWorkorder.Remove(workorder);
        }
    }
}
