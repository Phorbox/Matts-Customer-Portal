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
        static string jsonString = System.IO.File.ReadAllText("../JSON/Job.json");
        List<Job> testJob = Job.FromJson(jsonString);

        [HttpGet]
        public List<Job> Get()
        {
            var result = new List<Job>();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql = "SELECT * FROM Job";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(
                        new Job
                        {
                            Jobid = reader.GetInt64("Jobid"),
                            Projectid = reader.GetInt64("Projectid"),
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
        public Job Get(int id)
        {
            var result = new Job();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql = $"SELECT * FROM Job WHERE `Jobid` = {id}";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                result = new Job
                {
                    Jobid = reader.GetInt64("Jobid"),
                    Projectid = reader.GetInt64("Projectid"),
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
        public int Post([FromBody] Job job)
        {
            String insertValues = $"{job.Projectid}, {job.Clientid}, {job.Inputid}";
            String sql =
                $"INSERT INTO Job (Projectid, Clientid, Inputid) VALUES ({insertValues}))";
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
        public void Put(int id, [FromBody] Job job)
        {
            var index = testJob.FindIndex(x => x.Jobid == id);
            testJob[index] = job;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var job = testJob.FirstOrDefault(x => x.Jobid == id);
            testJob.Remove(job);
        }
    }
}
