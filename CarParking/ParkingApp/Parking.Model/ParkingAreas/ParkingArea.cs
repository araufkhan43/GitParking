using Parking.Model.ParkingZone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Model.ParkingArea
{
    [Table("parking_space")]
    public class ParkingArea
    {
        public int Id { get; set; }
        
        [Required]
        public string Parking_Space_Title { get; set; }
        public int Parking_Zone_Id { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Available { get; set; }

        [NotMapped]
        public string Parking_Zone_Title { get; set; }
      
        [NotMapped]
        public bool isException { get; set; }
        
        [NotMapped]
        public string exceptionMessage { get; set; }


        [NotMapped]
        public List<ParkingZone.ParkingZone> parkinZonelist { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
