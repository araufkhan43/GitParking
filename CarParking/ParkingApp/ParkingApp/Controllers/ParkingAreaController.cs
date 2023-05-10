using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking.Model.ParkingArea;
using ParkingApp.Models;
using System.Net.Http;
using Microsoft.Identity.Web;
using System.Text.Json;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net.Http.Json;
using Parking.Model.ParkingZone;
using Parking.Business.Business.Interface;
namespace ParkingApp.Controllers
{
    public class ParkingAreaController : Controller
    {
        private ITokenServce _tokenServce;
        public readonly HttpClient _httpClinet;
        public readonly string _APIScope = string.Empty;
        public readonly string _APIBaseAddress = string.Empty;
        public readonly ITokenAcquisition _tokenAcquistion;
        public readonly IParkingZoneBusiness _iparkingZoneBusiness;
        public ParkingAreaController(ITokenServce tokenServce, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration, IParkingZoneBusiness iparkingZoneBusiness)
        {

            _tokenServce = tokenServce;
            _httpClinet = httpClient;
            _tokenAcquistion = tokenAcquisition;
            _APIScope = configuration["APIConfig:APIScope"];
            _APIBaseAddress = configuration["APIConfig:APIBaseaddress"];
            _iparkingZoneBusiness = iparkingZoneBusiness;
        }
        public async Task<ActionResult> Index()
        {
            var response = await _httpClinet.GetAsync($"{_APIBaseAddress}/api/ParkingAreaApi/GetList");
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                string _content = await response.Content.ReadAsStringAsync();

                var x = JsonSerializer.Deserialize<List<ParkingArea>>(_content);
                return View(x);
            }


            return View(new List<ParkingArea>());
        }
        public async Task<ActionResult> Add()
        {
            ParkingArea obj=new ParkingArea();
            obj.parkinZonelist= _iparkingZoneBusiness.GetAll();
            return View(obj);
        }
        [HttpPost]
        public async Task<ActionResult> AddUpdate(ParkingArea obj)
        {
            var response = await _httpClinet.PostAsJsonAsync($"{_APIBaseAddress}/api/ParkingAreaApi/AddUpdate", obj);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (obj.Id == 0)
                {
                    TempData["Message"] = "Zone added Successfully";
                }
                if (obj.Id > 0)
                {
                    TempData["Message"] = "Zone updated Successfully";

                }
                return RedirectToAction("Index");

            }
            return View("Add");
        }
       
       
        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            var response = await _httpClinet.GetAsync($"{_APIBaseAddress}/api/ParkingAreaApi/GetListbyId?Id=" + Id);
            //return View(response.Content.ReadAsAsync<ParkingZone>().Result);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                string _content = await response.Content.ReadAsStringAsync();

                var x = JsonSerializer.Deserialize<ParkingArea>(_content);
                x.parkinZonelist = _iparkingZoneBusiness.GetAll();
                return View("Add", x);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _httpClinet.GetAsync($"{_APIBaseAddress}/api/ParkingAreaApi/Delete?Id=" + Id);
            //return View(response.Content.ReadAsAsync<ParkingZone>().Result);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {



                TempData["Message"] = "Area Inactve";
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }
    }
}
