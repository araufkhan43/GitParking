using Microsoft.Identity.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ParkingApp.Models
{
    public static class GlobalVariable
    {
        public static readonly HttpClient _httpClinet;
        public static readonly string _APIScope = string.Empty;
        public static readonly string _APIBaseAddress = string.Empty;
        public static readonly ITokenAcquisition _tokenAcquistion;

       
    }
}
