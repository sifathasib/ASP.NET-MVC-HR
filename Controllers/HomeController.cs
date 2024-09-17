using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using test.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace test.Controllers;

public class HomeController : Controller
{
    //private readonly ILogger<HomeController> _logger;

    // y

    public async Task<IActionResult> Index()
    {
        //var employees = await _dbContext.EMPLOYEES.ToListAsync();
        return View();
    }

    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    public IActionResult Privacy()
    {
        return View();
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
