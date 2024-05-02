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

    }
}
