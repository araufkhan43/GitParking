using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Parking.Business.Business.Interface;
using Parking.Model.Account;
using Parking.Model.ParkingZone;
using System.Text.Json;

namespace ParkingApp.WebApi.Controllers
{
   
    [ApiController]

    [Route("api/[controller]")]

    
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ParkingZoneAPIController : ControllerBase


    {
        private IParkingZoneBusiness iparkingZoneBusiness;
        public ParkingZoneAPIController(IParkingZoneBusiness iparkingZoneBusiness)
        {
            this.iparkingZoneBusiness = iparkingZoneBusiness;
        }

        
        [HttpGet("[action]")]
        public async  Task<string> GetList()
        {
            var x = "";
            String FunctionURL = "https://productfn.azurewebsites.net/api/GetZoneList?code=2xqsnPParNzqixptbb0bjgOwjQk3xEdgZ0PnSj3x38SWAzFuuBc0gA==";
            using (HttpClient _client = new HttpClient())
            {
                HttpResponseMessage _response = await _client.GetAsync(FunctionURL);
                string _content = await _response.Content.ReadAsStringAsync();
                //  x = JsonSerializer.Deserialize<List<ParkingZone>>(_content);
                //JsonSerializer.Deserialize<List<ParkingZone>>(_content);
                x = _content;
            }
            return x;
        }
    }
}
