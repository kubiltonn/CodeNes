using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using code_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace code_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _context = context;
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Dashboard()
    {
        var model = new DashboardViewModel();
        var client = _httpClientFactory.CreateClient();
        // API URL'lerini kendi ortamına göre güncelleyebilirsin
        model.StudentCount = await client.GetFromJsonAsync<int>("https://localhost:7133/api/Students/count");
        model.CourseCount = await client.GetFromJsonAsync<int>("https://localhost:7133/api/Courses/count");
        model.InstructorCount = await client.GetFromJsonAsync<int>("https://localhost:7133/api/Instructors/count");
        model.ClassroomCount = _context.Classrooms.Count();
        model.AnnouncementCount = _context.Announcements.Count();
        return View(model);
    }
}
