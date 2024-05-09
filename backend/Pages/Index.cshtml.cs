using backend.Models.Clientele;
using backend.Models.Project;
using backend.Models.Piece;
using backend.Models.Job;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async void OnGet()
    {
        // HttpClient JobClient = new HttpClient();
        // Job postTest = testJob[0];
        // var JobTask = JobClient.PostAsync("http://proxy/api/job",);
        // HttpResponseMessage JobResponse = JobTask.Result;
        // List<Job> JobList = new List<Job>();
        // if (JobResponse.IsSuccessStatusCode)
        // {
        //     Task<string> JobData = JobResponse.Content.ReadAsStringAsync();
        //     string jsonString = JobData.Result;
        //     JobList = Job.FromJson(jsonString);
        // }
        // ViewData["JobList"] = JobList;

        HttpClient JobClient = new HttpClient();

        var JobTask = JobClient.GetAsync("http://proxy/api/job");
        HttpResponseMessage JobResponse = JobTask.Result;
        List<Job> JobList = new List<Job>();
        if (JobResponse.IsSuccessStatusCode)
        {
            Task<string> JobData = JobResponse.Content.ReadAsStringAsync();
            string jsonString = JobData.Result;
            JobList = Job.FromJson(jsonString);
        }
        ViewData["JobList"] = JobList;
        // JobClient.Dispose();

        HttpClient PieceClient = new HttpClient();

        var PieceTask = PieceClient.GetAsync("http://proxy/api/piece");
        HttpResponseMessage PieceResponse = PieceTask.Result;
        List<Piece> PieceList = new List<Piece>();
        if (PieceResponse.IsSuccessStatusCode)
        {
            Task<string> PieceData = PieceResponse.Content.ReadAsStringAsync();
            string jsonString = PieceData.Result;
            PieceList = Piece.FromJson(jsonString);
        }
        ViewData["PieceList"] = PieceList;
        // PieceClient.Dispose();

        HttpClient ClienteleClient = new HttpClient();

        var ClienteleTask = ClienteleClient.GetAsync("http://proxy/api/clientele");
        HttpResponseMessage ClienteleResponse = ClienteleTask.Result;
        List<Clientele> ClienteleList = new List<Clientele>();
        if (ClienteleResponse.IsSuccessStatusCode)
        {
            Task<string> ClienteleData = ClienteleResponse.Content.ReadAsStringAsync();
            string jsonString = ClienteleData.Result;
            ClienteleList = Clientele.FromJson(jsonString);
        }
        ViewData["ClienteleList"] = ClienteleList;
        // ClienteleClient.Dispose();

        HttpClient ProjectClient = new HttpClient();

        var ProjectTask = ProjectClient.GetAsync("http://proxy/api/project");
        HttpResponseMessage ProjectResponse = ProjectTask.Result;
        List<Project> ProjectList = new List<Project>();
        if (ProjectResponse.IsSuccessStatusCode)
        {
            Task<string> ProjectData = ProjectResponse.Content.ReadAsStringAsync();
            string jsonString = ProjectData.Result;
            ProjectList = Project.FromJson(jsonString);
        }
        ViewData["ProjectList"] = ProjectList;
        // ProjectClient.Dispose();

        
    }
}
