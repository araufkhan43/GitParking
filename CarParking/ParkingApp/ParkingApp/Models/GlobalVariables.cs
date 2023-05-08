using Microsoft.Identity.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ParkingApp.Models
{
    public static class GlobalVariables
    {
        public readonly HttpClient _httpClinet;
        public readonly string _APIScope = string.Empty;
        public readonly string _APIBaseAddress = string.Empty;
        public readonly ITokenAcquisition _tokenAcquistion;

        public GlobalVariables(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
        {
            _httpClinet = httpClient;
            _tokenAcquistion = new ;
            _APIScope = configuration["APIConfig:APIScope"];
            _APIBaseAddress = configuration["APIConfig:APIBaseaddress"];
        }
    }
}
