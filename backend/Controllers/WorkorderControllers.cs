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
        static string jsonString = System.IO.File.ReadAllText("../JSON/Workorder.json");
        List<Workorder> testWorkorder = Workorder.FromJson(jsonString);

        [HttpGet]
        public List<Workorder> Get()
        {
            var result = new List<Workorder>();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql = "SELECT * FROM Workorder";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(
                        new Workorder
                        {
                            Workorderid = reader.GetInt64("Workorderid"),
                            Jobid = reader.GetInt64("Jobid"),
                            Clientid = reader.GetInt64("Clientid"),
                            Inputid = reader.GetInt64("Inputid"),
                            Status = reader.GetString("Status"),
                            DateApproved = Convert.IsDBNull(
                                reader.GetValue(reader.GetOrdinal("DateApproved"))
                            )
                                ? null
                                : reader.GetDateTime("DateApproved"), // Convert DateTime to string
                            DueDate = Convert.IsDBNull(
                                reader.GetValue(reader.GetOrdinal("DueDate"))
                            )
                                ? null
                                : reader.GetDateTime("DueDate"), // Convert DateTime to string
                            DateCreated = reader.GetDateTime("DateCreated")
                        }
                    );
                }
                reader.Close();
                connection.Close();
            }
            return result;
        }

        [HttpGet("{id}")]
        public Workorder Get(int id)
        {
            var result = new Workorder();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql = $"SELECT * FROM Workorder WHERE `Workorderid` = {id}";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                result = new Workorder
                {
                    Workorderid = reader.GetInt64("Workorderid"),
                    Jobid = reader.GetInt64("Jobid"),
                    Clientid = reader.GetInt64("Clientid"),
                    Inputid = reader.GetInt64("Inputid"),
                    Status = reader.GetString("Status"),
                    DateApproved = Convert.IsDBNull(
                        reader.GetValue(reader.GetOrdinal("DateApproved"))
                    )
                        ? null
                        : reader.GetDateTime("DateApproved"), // Convert DateTime to string
                    DueDate = Convert.IsDBNull(reader.GetValue(reader.GetOrdinal("DueDate")))
                        ? null
                        : reader.GetDateTime("DueDate"), // Convert DateTime to string
                    DateCreated = reader.GetDateTime("DateCreated")
                };

                reader.Close();
                connection.Close();
            }
            return result;
        }

        [HttpPost]
        public int Post([FromBody] Workorder workorder)
        {
            String insertValues = $"{workorder.Jobid}, {workorder.Clientid}, {workorder.Inputid}";
            String sql =
                $"INSERT INTO Workorder (Jobid, Clientid, Inputid) VALUES ({insertValues}))";
            // try
            // {
            //     using (
            //         MySqlConnection connection = new MySqlConnection(
            //             CommonConnection.connectionString
            //         )
            //     )
            //     {
            //         connection.Open();
            //         using MySqlCommand cmd = new MySqlCommand(sql, connection);
            //         cmd.ExecuteNonQuery();
            //         connection.Close();
            //     }
            // }
            // catch (Exception e)
            // {
            //     return 0;
            // }

            return 1;
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
