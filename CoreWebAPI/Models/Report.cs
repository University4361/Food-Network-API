using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models
{
    [DataContract]
    public class Report
    {
        [DataMember]
        public int ID { get; set; }
        
        [DataMember]
        public float Profit { get; set; }

        [DataMember]
        [Required]
        public string Comment { get; set; }
        
        [DataMember]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReportDate { get; set; }
        
        [DataMember]
        public double Distance { get; set; }

        [IgnoreDataMember]
        [Required]
        public Courier Courier { get; set; }
    }
}
