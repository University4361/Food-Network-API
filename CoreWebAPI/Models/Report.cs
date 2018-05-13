using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models
{
    public class Report
    {
        public int ID { get; set; }
        
        public float Profit { get; set; }

        [Required]
        public string Comment { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReportDate { get; set; }
        
        public double Distance { get; set; }

        [Required]
        public Courier Courier { get; set; }
    }
}
