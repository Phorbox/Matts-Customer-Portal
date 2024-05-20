using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public class JobController : ControllerBase
    {
        static string jsonString = System.IO.File.ReadAllText("../JSON/Job.json");
        List<Job> testJob = Job.FromJsonList(jsonString);

        [HttpGet]
        public List<Job> Get()
        {
            var result = new List<Job>();
            using (
                MySqlConnection connection = new MySqlConnection(CommonConnection.connectionString)
            )
            {
                connection.Open();
                string sql =
                    @"  
                        SELECT *
                        FROM Job
                        JOIN Project ON Job.Projectid = Project.Projectid
                        JOIN Clientele ON Job.Clienteleid = Clientele.Clienteleid
                        JOIN Input ON Job.Inputid = Input.Inputid;";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ConvertReaderToJob(reader));
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

                string sql =
                    $@"  
                        SELECT *
                        FROM Job
                        JOIN Project ON Job.Projectid = Project.Projectid
                        JOIN Clientele ON Job.Clienteleid = Clientele.Clienteleid
                        JOIN Input ON Job.Inputid = Input.Inputid
                        WHERE `Jobid` = {id};";

                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                result = ConvertReaderToJob(reader);

                reader.Close();
                connection.Close();
            }
            return result;
        }

        // [HttpPost]
        // public int Post([FromBody] string body)
        // {
        //     Job job = Job.FromJson(body);
        //     if (job.ClienteleName != null)
        //     {
        //          HttpClient ClienteleClient = new HttpClient();
        //         var ClienteleTask = ClienteleClient.GetAsync("http://proxy/api/clientele");
        //         HttpResponseMessage ClienteleResponse = ClienteleTask.Result;
        //         List<Clientele> ClienteleList = new List<Clientele>();
        //         if (ClienteleResponse.IsSuccessStatusCode)
        //         {
        //             Task<string> ClienteleData = ClienteleResponse.Content.ReadAsStringAsync();
        //             string jsonString = ClienteleData.Result;
        //             ClienteleList = Clientele.FromJson(jsonString);
        //             job.Clienteleid = ClienteleList.FirstOrDefault(x => x.ClienteleName == job.ClienteleName).Clienteleid;
        //         }
        //     }

        //     string jobSql =
        //         @$"
        //         INSERT INTO Job
        //         (
        //             Projectid,
        //             Clienteleid,
        //             Inputid,
        //         )
        //         VALUES
        //         (
        //             {job.Projectid},
        //             {job.Clienteleid},
        //             {job.Inputid}
        //         );";

        //     string InputSql =
        //         @$"
        //         INSERT INTO Input
        //         (
        //             Filename,
        //             StoragePriority,
        //             InputPdf
        //         )
        //         VALUES
        //         (
        //             '{job.Filename}',
        //             '{job.InputPdf}'
        //         );";

        //     return 1;
        // }

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

        private static Job ConvertReaderToJob(MySqlDataReader reader)
        {
            Job job = new Job();

            job.Jobid = reader.GetInt64("Jobid");
            job.Projectid = reader.GetInt64("Projectid");
            job.Clienteleid = reader.GetInt64("Clienteleid");
            job.Inputid = reader.GetInt64("Inputid");
            job.Status = reader.GetString("Status");
            job.DateApproved = CommonConnection.getNullableDate(reader, "DateApproved");
            job.DueDate = CommonConnection.getNullableDate(reader, "DueDate");
            job.DateCreated = reader.GetDateTime("DateCreated");
            job.ProjectName = reader.GetString("ProjectName");
            job.SlaOveride = CommonConnection.getNullableLong(reader, "SlaOveride");
            job.Approval = CommonConnection.getNullableString(reader, "Approval");
            job.ClienteleName = reader.GetString("ClienteleName");
            job.RetentionLength = reader.GetInt64("RetentionLength");
            job.SlaDueDate = reader.GetInt64("SlaDueDate");
            job.ParentId = CommonConnection.getNullableLong(reader, "ParentId");
            job.Filename = reader.GetString("Filename");
            job.StoragePriority = reader.GetString("StoragePriority");
            // job.InputPdf = reader.GetString("InputPdf");
            job.InputPdf = null;
            return job;
        }
    }
}
