using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ParkingFunctionApp.Connection;
using ParkingFunctionApp.Modle;
using System.Collections.Generic;

namespace ParkingFunctionApp.ParkingZone
{
    public static class GetZone
    {
        [FunctionName("GetZone")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            int Id = int.Parse(req.Query["Id"]);

            string _statement = String.Format("SELECT Id,Parking_Zone_Title,Is_Active from parking_zone WHERE Id={0}", Id);
            SqlConnection _connection = Utility.GetConnection();

            _connection.Open();

            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            ParkingZones obj = new ParkingZones();

            try
            {
                using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
                {
                    _reader.Read();
                    obj.Id = _reader.GetInt32(0);
                    obj.Parking_Zone_Title = _reader.GetString(1);
                    obj.Is_Active = _reader.GetBoolean(2);
                    var response = obj;
                    return new OkObjectResult(JsonConvert.SerializeObject(response));
                }
            }
            catch (Exception ex)
            {
                var response = "No Records found";
                return new OkObjectResult(response);
            }
            _connection.Close();

        }


        [FunctionName("GetZoneList")]
        public static async Task<IActionResult> Runlist(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get data from the database");
            List<ParkingZones> obj_lst = new List<ParkingZones>();
            string _statement = "SELECT  Id,Parking_Zone_Title,Is_Active from parking_zone";
            SqlConnection _connection = Utility.GetConnection();

            _connection.Open();

            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    ParkingZones obj = new ParkingZones()
                    {
                        Id = _reader.GetInt32(0),
                        Parking_Zone_Title = _reader.GetString(1),
                        Is_Active = _reader.GetBoolean(2)
                    };

                    obj_lst.Add(obj);
                }
            }
            _connection.Close();

            //  return new OkObjectResult(_product_lst);
            return new OkObjectResult(JsonConvert.SerializeObject(obj_lst));

        }
    }
}
