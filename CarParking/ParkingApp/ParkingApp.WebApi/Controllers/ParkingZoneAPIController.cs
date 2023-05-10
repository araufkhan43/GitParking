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
        [HttpGet("[action]")]
        public async Task<string> GetListbyId(int Id)
        {
            var x = "";
            String FunctionURL = "https://productfn.azurewebsites.net/api/GetZone?code=X-BDsV_sOUoa7e105at65yXbtwsBCM1qK2miqy5vmqa5AzFuPfhbjQ==&Id="+ Id + "";
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
        [HttpGet("[action]")]
        public async Task<string> Delete(int Id)
        {
            var x = "";
            String FunctionURL = "https://productfn.azurewebsites.net/api/DeleteZone?code=gEgIMIFIUaizr9JVz6VvAJo8_vuC_15dHfLAf96fU4AXAzFu3P4Alg==&Id="+Id+"";
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
        [HttpPost("[action]")]
        public async Task<string> AddUpdate(ParkingZone obj)
        {
            var msg = "";
            if (obj.Id > 0)
            {
                String FunctionURL = "https://productfn.azurewebsites.net/api/UpdateZone?code=bshhYJe3_OBwZFzdYmzIzcNIb65TEkavRSzcdrkf5xgDAzFuSLzwyA==&Id=" + obj.Id + "&Parking_Zone_Title=" + obj.Parking_Zone_Title + "&Is_Active=" + obj.Is_Active + "";
                using (HttpClient _client = new HttpClient())
                {

                    msg = "Record updated successfully";
                    //newtoJsonConvert.SerializeObject(obj);
                    var jsonString = JsonSerializer.Serialize(obj);
                    var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage _response = await _client.PostAsync(FunctionURL, httpContent);
                    string _content = await _response.Content.ReadAsStringAsync();
                    return msg;
                }
            }
            else
            {
                String FunctionURL = "https://productfn.azurewebsites.net/api/AddZone?code=bG33yV24g_jpZaCUyOwdB-2CHqWs4Hyt2mXsFznt4wj3AzFu0pQiLQ==";
                using (HttpClient _client = new HttpClient())
                {

                    msg = "Record Added successfully";
                    //newtoJsonConvert.SerializeObject(obj);
                    var jsonString = JsonSerializer.Serialize(obj);
                    var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage _response = await _client.PostAsync(FunctionURL, httpContent);
                    string _content = await _response.Content.ReadAsStringAsync();
                    //var x = JsonSerializer.Deserialize<Product>(_content);
                    return msg;
                }


            }
        }
    }
}
