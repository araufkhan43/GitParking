using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace ParkingFunctionApp.Connection
{
    public class Utility
    {
        public static SqlConnection GetConnection()
        {

            string connectionString = "Server=tcp:abdul-azure.database.windows.net,1433;Initial Catalog=AssignmentDatabase;Persist Security Info=False;User ID=system;Password=1234567890!@#a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; //Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
            return new SqlConnection(connectionString);
        }
    }
}
