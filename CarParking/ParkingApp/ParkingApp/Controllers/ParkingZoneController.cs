using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking.Model.ParkingZone;
using ParkingApp.Models;
using System.Net.Http;
using Microsoft.Identity.Web;
using System.Text.Json;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ParkingApp.Controllers
{
    public class ParkingZoneController : Controller
    {
       // private readonly ILogger<HomeController> _logger;
        private ITokenServce _tokenServce;
        public readonly HttpClient _httpClinet;
        public readonly string _APIScope = string.Empty;
        public readonly string _APIBaseAddress = string.Empty;
        public readonly ITokenAcquisition _tokenAcquistion;
        public ParkingZoneController(ITokenServce tokenServce, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
        {
            
            _tokenServce = tokenServce;
            _httpClinet = httpClient;
            _tokenAcquistion = tokenAcquisition;
            _APIScope = configuration["APIConfig:APIScope"];
            _APIBaseAddress = configuration["APIConfig:APIBaseaddress"];

        }
        public async Task<ActionResult> Index()
        {
            //var view = await _tokenServce.Get();
              
            var response = await _httpClinet.GetAsync($"{_APIBaseAddress}/api/ParkingZoneAPI/GetList");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
                string _content = await response.Content.ReadAsStringAsync();

                var x = JsonSerializer.Deserialize<List<ParkingZone>>(_content);
                return View(x);
            }

           
            return View(new List<ParkingZone>());
        }
    }
}
