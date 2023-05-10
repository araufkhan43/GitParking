using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParkingFunctionApp.Connection;
using ParkingFunctionApp.Modle;
using System.Data.SqlClient;
using System.Data;

namespace ParkingFunctionApp.ParkingZone
{
    public static class UpdateZone
    {
        [FunctionName("UpdateZone")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ParkingZones data = JsonConvert.DeserializeObject<ParkingZones>(requestBody);

            SqlConnection connection = Utility.GetConnection();

            connection.Open();

            //string statement = "INSERT INTO parking_zone(Parking_Zone_Title,Is_Available) VALUES(@param2,@param3)";
            string statement = "update parking_zone set Parking_Zone_Title='" + data.Parking_Zone_Title + "',Is_Active='" + data.Is_Active+ "',CreatedOn='"+DateTime.Now+"' Where Id='" + data.Id + "'";

            using (SqlCommand command = new SqlCommand(statement, connection))
            {
                command.Parameters.Add("@param1", SqlDbType.Int).Value = data.Id;
                command.Parameters.Add("@param2", SqlDbType.NVarChar, 1000).Value = data.Parking_Zone_Title;
                command.Parameters.Add("@param3", SqlDbType.Bit).Value = data.Is_Active;
                command.Parameters.Add("@param4", SqlDbType.DateTime).Value = DateTime.Now;


                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

            }
            return new OkObjectResult("Zone Updated");
        }



        [FunctionName("DeleteZone")]
        public static async Task<IActionResult> RunDelete(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            int Id = int.Parse(req.Query["Id"]);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ParkingZones data = JsonConvert.DeserializeObject<ParkingZones>(requestBody);

            SqlConnection connection = Utility.GetConnection();

            connection.Open();

            //string statement = "INSERT INTO parking_zone(Parking_Zone_Title,Is_Available) VALUES(@param2,@param3)";
            string statement = "update parking_zone set Is_Active='false',CreatedOn='"+DateTime.Now+"' Where Id='" + Id + "'";

            using (SqlCommand command = new SqlCommand(statement, connection))
            {
                command.Parameters.Add("@param1", SqlDbType.Int).Value = Id;
                
                command.Parameters.Add("@param3", SqlDbType.Bit).Value = 0;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

            }
            return new OkObjectResult("Zone Deleted");
        }
    }
}
