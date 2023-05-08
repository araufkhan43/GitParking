using Microsoft.AspNetCore.Mvc;
using ParkingApp.Models;
using System.Diagnostics;
namespace ParkingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ITokenServce _tokenServce;
        public HomeController(ILogger<HomeController> logger, ITokenServce tokenServce)
        {
            _logger = logger;
            _tokenServce = tokenServce;
        }

        public async Task<ActionResult> Index()
        {

            var view = await _tokenServce.Get();

            IList<string> obj = new List<string>();

            obj.Add(view);

            return View(obj as IEnumerable<string>);
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
    }
}
