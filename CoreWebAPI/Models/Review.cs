using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models
{
    [DataContract]
    public class Review
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Content { get; set; }

        [DataMember]
        [Required]
        public float Rate { get; set; }

        [IgnoreDataMember]
        [Required]
        public Customer Customer { get; set; }

        [IgnoreDataMember]
        [Required]
        public Courier Courier { get; set; }
    }
}
