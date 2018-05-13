using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Country { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string City { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Street { get; set; }

        [DataMember]
        [Required]
        public int HomeNumber { get; set; }

        [IgnoreDataMember]
        public Customer Customer { get; set; }

        [IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
    }
}
