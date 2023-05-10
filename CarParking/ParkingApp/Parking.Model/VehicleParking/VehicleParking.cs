using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model.VehicleParking
{
    [Table("vehicle_parking")]
    public class VehicleParking
    {
        [Key]
        public int Id { get;set; }
        public int Parking_Zone_Id { get;set; }
        public int Parking_Space_Id { get;set; }
        public DateTime Booking_Date_Time { get;set; }
        public DateTime Release_Date_Time { get;set; }
    }
}
