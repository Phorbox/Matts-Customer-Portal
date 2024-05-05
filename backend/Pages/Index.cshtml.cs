using backend.Models.Clientele;
using backend.Models.Job;
using backend.Models.Piece;
using backend.Models.Workorder;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public string message = "This message came from the index's model.";

    public async void OnGet()
    {
        HttpClient WorkorderClient = new HttpClient();

        var WorkorderTask = WorkorderClient.GetAsync("http://proxy/api/workorder");
        HttpResponseMessage WorkorderResponse = WorkorderTask.Result;
        List<Workorder> WorkorderList = new List<Workorder>();
        if (WorkorderResponse.IsSuccessStatusCode)
        {
            Task<string> WorkorderData = WorkorderResponse.Content.ReadAsStringAsync();
            string jsonString = WorkorderData.Result;
            WorkorderList = Workorder.FromJson(jsonString);
        }
        ViewData["WorkorderList"] = WorkorderList;
        // WorkorderClient.Dispose();

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

        
    }
}
