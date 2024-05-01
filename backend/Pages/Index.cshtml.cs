using Microsoft.AspNetCore.Mvc.RazorPages;
using backend.Models;

namespace backend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public string message = "This message came from the index's model.";

    public void OnGet()
    {
        HttpClient client = new HttpClient();
      


        var task = client.GetAsync("http://proxy//api/workorder");
        HttpResponseMessage response = task.Result;
        List<Workorder> WorkorderList = new List<Workorder>();
        if (response.IsSuccessStatusCode)
        {
            Task<string> data = response.Content.ReadAsStringAsync();
            string jsonString = data.Result;
            WorkorderList = Workorder.FromJson(jsonString);
        }
        ViewData["WorkorderList"] = WorkorderList;
    }
}
