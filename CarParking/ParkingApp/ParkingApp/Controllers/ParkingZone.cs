using Microsoft.AspNetCore.Mvc;

namespace ParkingApp.Controllers
{
    public class ParkingZone : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
