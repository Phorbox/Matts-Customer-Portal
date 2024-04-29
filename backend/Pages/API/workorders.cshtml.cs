using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static backend.Common.MySQLConnection;

namespace backend.Pages;

public class WorkordersModel : PageModel
{
    private readonly ILogger<WorkordersModel> _logger;

    public WorkordersModel(ILogger<WorkordersModel> logger)
    {
        _logger = logger;
    }

    // public String Thing = "Yahoo";
    public List<string> Thing = GetTitles();

    public void OnGet()
    {
    }
}
