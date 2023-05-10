using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ParkingFunctionApp.Connection;
using ParkingFunctionApp.Modle;
using System;

namespace ParkingFunctionApp.ParkingZone
{
    public static class AddZone
    {
        [FunctionName("AddZone")]
        public static async Task<IActionResult> Run(
              [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
              ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ParkingZones data = JsonConvert.DeserializeObject<ParkingZones>(requestBody);

            SqlConnection connection = Utility.GetConnection();

            connection.Open();
            
        string statement = "INSERT INTO parking_zone(Parking_Zone_Title,Is_Active,CreatedOn) VALUES(@param2,@param3,@param4)";

            using (SqlCommand command = new SqlCommand(statement, connection))
            {
                command.Parameters.Add("@param1", SqlDbType.Int).Value = 0;
                command.Parameters.Add("@param2", SqlDbType.NVarChar, 1000).Value = data.Parking_Zone_Title;
                command.Parameters.Add("@param3", SqlDbType.Bit).Value = data.Is_Active;
                command.Parameters.Add("@param4", SqlDbType.DateTime).Value = DateTime.Now;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

            }

            return new OkObjectResult("Zone added");

           
        }
    }
}
