using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Model.ParkingArea;
using Parking.Model.ParkingZone;
using System.Text.Json;
namespace ParkingApp.WebApi.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ParkingAreaApiController : ControllerBase
    {

        [HttpGet("[action]")]
        public async Task<string> GetList()
        {
            var x = "";
            String FunctionURL = "https://productfn.azurewebsites.net/api/GetParkingList?code=V-pOsjE1e3O9cVvKRJ6m7OhSQuOilsxZIq4bfaIKxEzKAzFu8HaL6w==";
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
            String FunctionURL = "https://productfn.azurewebsites.net/api/GetParkingListById?code=fx8bjiCd8RAZyMSN5M0Fvv7KfmsxXzNJ2YA0VHMEW3N3AzFuCyptMg==&Id=" + Id + "";
            using (HttpClient _client = new HttpClient())
            {
                HttpResponseMessage _response = await _client.GetAsync(FunctionURL);
                string _content = await _response.Content.ReadAsStringAsync();
                //  x = JsonSerializer.Deserialize<List<ParkingZone>>(_content);
                //JsonSerializer.Deserialize<List<ParkingZone>>(_content);aa
                x = _content;
            }
            return x;
        }
        [HttpPost("[action]")]
        public async Task<string> AddUpdate(ParkingArea obj)
        {
            var msg = "";
            if (obj.Id > 0)
            {
                String FunctionURL = "https://productfn.azurewebsites.net/api/UpdateParking?code=NpHnlTuozVf-MNsH3NQKpGEsSd0hVEykiTn1BjJ0ygT6AzFuy2GUrg==&Id=" + obj.Id + "&Parking_Zone_Id=" + obj.Parking_Zone_Id + "&Is_Active=" + obj.Is_Active + "&Parking_Space_Title=" + obj.Parking_Space_Title+"";
                using (HttpClient _client = new HttpClient())
                {

                    msg = "Record updated successfully";
                    //newtoJsonConvert.SerializeObject(obj);
                    var jsonString = JsonSerializer.Serialize(obj);
                    var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage _response = await _client.PutAsync(FunctionURL, httpContent);
                    string _content = await _response.Content.ReadAsStringAsync();
                    return msg;
                }
            }
            else
            {
                String FunctionURL = "https://productfn.azurewebsites.net/api/AddParking?code=cqypLkGrFULIEItns1ZAdQtI8IPlSs6U3NdpiMB83LdfAzFu1PC60w==";
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
        [HttpGet("[action]")]
        public async Task<string> Delete(int Id)
        {
            var x = "";
            String FunctionURL = "https://productfn.azurewebsites.net/api/DeleteParkingSpace?code=8B26Vrt8_yg3yOqSkkTawwcRSugCW7grxjr7csZmRc0AAzFuaRKkuQ==&Id=" + Id + "";
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
