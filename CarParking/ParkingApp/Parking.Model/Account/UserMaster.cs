using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Parking.Model.Account
{

    [Table("user")]
    public  class UserMaster
    {
        [Key]
        public int user_id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name")]
        [MaxLength(12)]
        public string name { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MaxLength(50)]
        public string email { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MaxLength(12)]
        public string password { get; set; }
       
        
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(12)]
        public string type { get; set; }
        [NotMapped]
        
        public string file_path { get; set; }

        public bool IsActive { get; set; }


        [NotMapped]
        public IFormFile File { get; set; }

    }
}
