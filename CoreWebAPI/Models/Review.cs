using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public float Rate { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Courier Courier { get; set; }
    }
}
