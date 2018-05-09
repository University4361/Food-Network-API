using System;
using System.ComponentModel.DataAnnotations;

namespace CoreWebAPI.Models
{
    public class Courier
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        
        public float Rate { get; set; }

        [StringLength(256)]
        public string PhotoUrl { get; set; }
    }
}
