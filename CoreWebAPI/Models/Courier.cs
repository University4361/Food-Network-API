using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CoreWebAPI.Models
{
    [DataContract]
    public class Courier
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [IgnoreDataMember]
        [Required]
        [StringLength(256)]
        public string Login { get; set; }

        [IgnoreDataMember]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public float Rate { get; set; }

        [DataMember]
        [StringLength(256)]
        public string PhotoUrl { get; set; }

        [IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }

        [IgnoreDataMember]
        public ICollection<Report> Reports { get; set; }

        [IgnoreDataMember]
        public ICollection<Review> Reviews { get; set; }

        [IgnoreDataMember]
        public CourierToken CourierToken { get; set; }
    }
}
