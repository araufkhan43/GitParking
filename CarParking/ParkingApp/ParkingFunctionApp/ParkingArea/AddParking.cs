using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Parking.Model.ParkingArea;
using System.Data.SqlClient;
using System.Data;
using ParkingFunctionApp.Connection;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ParkingFunctionApp.ParkingArea
{
    public static class AddParking
    {
        [FunctionName("AddParking")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,"post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Parking.Model.ParkingArea.ParkingArea  data = JsonConvert.DeserializeObject<Parking.Model.ParkingArea.ParkingArea>(requestBody);
            SqlConnection connection = Utility.GetConnection();
            SqlCommand sqlCmd = new SqlCommand("[SP_ParkingArea]", connection);

            sqlCmd.CommandTimeout = 1000;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@OpCode", SqlDbType.Int).Value = 11;

           
            sqlCmd.Parameters.AddWithValue("@Id", data.Id);

            sqlCmd.Parameters.AddWithValue("@Parking_Zone_Id", data.Parking_Zone_Id);
            sqlCmd.Parameters.AddWithValue("@Parking_Space_Title", data.Parking_Space_Title);
            sqlCmd.Parameters.AddWithValue("@Is_Active", data.Is_Active);

            sqlCmd.Parameters.Add("@isException", SqlDbType.Bit);
            sqlCmd.Parameters["@isException"].Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@exceptionMessage", SqlDbType.NVarChar, 500);
            sqlCmd.Parameters["@exceptionMessage"].Direction = ParameterDirection.Output;
            SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
            DataTable dt= new DataTable();
            sqlAdp.Fill(dt);
           
            data.isException = Convert.ToBoolean(sqlCmd.Parameters["@isException"].Value);
            data.exceptionMessage = sqlCmd.Parameters["@exceptionMessage"].Value.ToString();
            if (!data.isException)
            {
                return new OkObjectResult("Record Saved");


            }
            else
            {
                return new OkObjectResult((data.exceptionMessage));

            }
           
        }
        [FunctionName("UpdateParking")]
        public static async Task<IActionResult> RunUpdate(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
           ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Parking.Model.ParkingArea.ParkingArea data = JsonConvert.DeserializeObject<Parking.Model.ParkingArea.ParkingArea>(requestBody);
            SqlConnection connection = Utility.GetConnection();
            SqlCommand sqlCmd = new SqlCommand("[SP_ParkingArea]", connection);

            sqlCmd.CommandTimeout = 1000;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@OpCode", SqlDbType.Int).Value = 21;


            sqlCmd.Parameters.AddWithValue("@Id", data.Id);

            sqlCmd.Parameters.AddWithValue("@Parking_Zone_Id", data.Parking_Zone_Id);
            sqlCmd.Parameters.AddWithValue("@Parking_Space_Title", data.Parking_Space_Title);
            sqlCmd.Parameters.AddWithValue("@Is_Active", data.Is_Active);

            sqlCmd.Parameters.Add("@isException", SqlDbType.Bit);
            sqlCmd.Parameters["@isException"].Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@exceptionMessage", SqlDbType.NVarChar, 500);
            sqlCmd.Parameters["@exceptionMessage"].Direction = ParameterDirection.Output;
            SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            sqlAdp.Fill(dt);
           
            data.isException = Convert.ToBoolean(sqlCmd.Parameters["@isException"].Value);
            data.exceptionMessage = sqlCmd.Parameters["@exceptionMessage"].Value.ToString();
            if (!data.isException)
            {
                return new OkObjectResult("Record Updated");


            }
            else
            {
                return new OkObjectResult((data.exceptionMessage));

            }
          
        }
        [FunctionName("GetParkingList")]
        public static async Task<IActionResult> RunGetAll(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
          ILogger log)
        {

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Parking.Model.ParkingArea.ParkingArea data = JsonConvert.DeserializeObject<Parking.Model.ParkingArea.ParkingArea>(requestBody);
            SqlConnection connection = Utility.GetConnection();
            SqlCommand sqlCmd = new SqlCommand("[SP_ParkingArea]", connection);

            sqlCmd.CommandTimeout = 1000;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@OpCode", SqlDbType.Int).Value = 41;


            //sqlCmd.Parameters.AddWithValue("@Id", data.Id);

            //sqlCmd.Parameters.AddWithValue("@Parking_Zone_Id", data.Parking_Zone_Id);
            //sqlCmd.Parameters.AddWithValue("@Parking_Space_Title", data.Parking_Space_Title);
            //sqlCmd.Parameters.AddWithValue("@Is_Active", data.Is_Active);

            sqlCmd.Parameters.Add("@isException", SqlDbType.Bit);
            sqlCmd.Parameters["@isException"].Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@exceptionMessage", SqlDbType.NVarChar, 500);
            sqlCmd.Parameters["@exceptionMessage"].Direction = ParameterDirection.Output;

            SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            sqlAdp.Fill(dt);

            Parking.Model.ParkingArea.ParkingArea data = new Parking.Model.ParkingArea.ParkingArea();
            data.isException = Convert.ToBoolean(sqlCmd.Parameters["@isException"].Value);
            data.exceptionMessage = sqlCmd.Parameters["@exceptionMessage"].Value.ToString();
            List<Parking.Model.ParkingArea.ParkingArea> obj_lst = new List<Parking.Model.ParkingArea.ParkingArea>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var x = new Parking.Model.ParkingArea.ParkingArea();
                    x.Id = Convert.ToInt32(dr["Id"].ToString());
                    x.Parking_Space_Title = dr["Parking_Space_Title"].ToString();
                    x.Parking_Zone_Id = Convert.ToInt32(dr["Parking_Zone_Id"].ToString());
                    x.Is_Active = Convert.ToBoolean(dr["Is_Active"].ToString());
                    x.Parking_Zone_Title = dr["Parking_Zone_Title"].ToString();
                    obj_lst.Add(x);
                }
                
            }
            if (!data.isException)
            {
                return new OkObjectResult((JsonConvert.SerializeObject(obj_lst)));


            }
            else
            {
                return new OkObjectResult((data.exceptionMessage));

            }
        }
        [FunctionName("GetParkingListById")]
        public static async Task<IActionResult> RunGetById(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
          ILogger log)
        {
            int Id = int.Parse(req.Query["Id"]);
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Parking.Model.ParkingArea.ParkingArea data = JsonConvert.DeserializeObject<Parking.Model.ParkingArea.ParkingArea>(requestBody);
            SqlConnection connection = Utility.GetConnection();
            SqlCommand sqlCmd = new SqlCommand("[SP_ParkingArea]", connection);

            sqlCmd.CommandTimeout = 1000;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@OpCode", SqlDbType.Int).Value = 42;


            sqlCmd.Parameters.AddWithValue("@Id", Id);

            sqlCmd.Parameters.Add("@isException", SqlDbType.Bit);
            sqlCmd.Parameters["@isException"].Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@exceptionMessage", SqlDbType.NVarChar, 500);
            sqlCmd.Parameters["@exceptionMessage"].Direction = ParameterDirection.Output;
            

            SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            sqlAdp.Fill(dt);
            Parking.Model.ParkingArea.ParkingArea data = new Parking.Model.ParkingArea.ParkingArea();




            data.isException = Convert.ToBoolean(sqlCmd.Parameters["@isException"].Value);
            data.exceptionMessage = sqlCmd.Parameters["@exceptionMessage"].Value.ToString();
            var x = new Parking.Model.ParkingArea.ParkingArea();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    x.Id = Convert.ToInt32(dr["Id"].ToString());
                    x.Parking_Space_Title = dr["Parking_Space_Title"].ToString();
                    x.Parking_Zone_Id = Convert.ToInt32(dr["Parking_Zone_Id"].ToString());
                    x.Is_Active = Convert.ToBoolean(dr["Is_Active"].ToString());
                   
                }

            }
            if (!data.isException)
            {
                return new OkObjectResult((JsonConvert.SerializeObject(x)));

            }
            else
            {
                return new OkObjectResult((data.exceptionMessage));

            }
        }

        [FunctionName("DeleteParkingSpace")]
        public static async Task<IActionResult> RunDeleteId(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
          ILogger log)
        {
            int Id = int.Parse(req.Query["Id"]);
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Parking.Model.ParkingArea.ParkingArea data = JsonConvert.DeserializeObject<Parking.Model.ParkingArea.ParkingArea>(requestBody);
            SqlConnection connection = Utility.GetConnection();
            SqlCommand sqlCmd = new SqlCommand("[SP_ParkingArea]", connection);

            sqlCmd.CommandTimeout = 1000;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@OpCode", SqlDbType.Int).Value = 31;


            sqlCmd.Parameters.AddWithValue("@Id", Id);
            sqlCmd.Parameters.Add("@isException", SqlDbType.Bit);
            sqlCmd.Parameters["@isException"].Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@exceptionMessage", SqlDbType.NVarChar, 500);
            sqlCmd.Parameters["@exceptionMessage"].Direction = ParameterDirection.Output;

            SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            sqlAdp.Fill(dt);
            Parking.Model.ParkingArea.ParkingArea data=new Parking.Model.ParkingArea.ParkingArea();
            data.isException = Convert.ToBoolean(sqlCmd.Parameters["@isException"].Value);
            data.exceptionMessage = sqlCmd.Parameters["@exceptionMessage"].Value.ToString();

            return new OkObjectResult("Zone Deleted");
        }

    }

}
