using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using Parking.Business.Business.Interface;
using Parking.Model.ParkingArea;
using Parking.Model.VehicleParking;
using System.Net.Http.Json;

namespace ParkingApp.Controllers
{
    public class Dashboard : Controller
    {
        private ITokenServce _tokenServce;
        public readonly HttpClient _httpClinet;
        public readonly string _APIScope = string.Empty;
        public readonly string _APIBaseAddress = string.Empty;
        public readonly ITokenAcquisition _tokenAcquistion;
        public readonly IParkingZoneBusiness _iparkingZoneBusiness;
        public Dashboard(ITokenServce tokenServce, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration, IParkingZoneBusiness iparkingZoneBusiness)
        {

            _tokenServce = tokenServce;
            _httpClinet = httpClient;
            _tokenAcquistion = tokenAcquisition;
            _APIScope = configuration["APIConfig:APIScope"];
            _APIBaseAddress = configuration["APIConfig:APIBaseaddress"];
            _iparkingZoneBusiness = iparkingZoneBusiness;
        }
        public IActionResult Index()
        {
            ParkingArea obj = new ParkingArea();
            obj.parkinZonelist = _iparkingZoneBusiness.GetAll();
            return View(obj);
        }
        [HttpGet]
        public async Task<string> GetAllList(int id)
        {
            var response = await _httpClinet.GetAsync($"{_APIBaseAddress}/api/DashboardApi/GetAllParkingSlot?Id="+id);
            var json = "";
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               
                json = await response.Content.ReadAsStringAsync();

                
                
                }


            return json;
        }
        [HttpGet]
        public async Task<string> BookParkingSpace(int zoneId,int spaceId,DateTime bookingTime,DateTime releaseTime)
        {
            VehicleParking obj=new VehicleParking();
            obj.Parking_Space_Id = spaceId;
            obj.Parking_Zone_Id = zoneId;
            obj.Booking_Date_Time = bookingTime;
            
            var response = await _httpClinet.PostAsJsonAsync($"{_APIBaseAddress}/api/DashboardApi/BookParkingSpace",obj);
            var json = "";
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                json = await response.Content.ReadAsStringAsync();



            }


            return json;
        }
    }
}
