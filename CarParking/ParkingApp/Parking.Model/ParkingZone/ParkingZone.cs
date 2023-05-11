using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Parking.Model.ParkingZone
{
    [Table("parking_zone")]
    public class ParkingZone
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(40)]
        public string Parking_Zone_Title { get; set; }

        public bool Is_Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
