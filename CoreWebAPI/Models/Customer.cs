using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Login { get; set; }
        
        [DataMember]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [DataMember]
        [StringLength(256)]
        public string PhotoUrl { get; set; }

        [IgnoreDataMember]
        public ICollection<Address> Addresses { get; set; }

        [IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }

        [IgnoreDataMember]
        public ICollection<Review> Reviews { get; set; }
    }
}
