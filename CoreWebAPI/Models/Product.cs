using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
        
        [DataMember]
        public float Price { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        [StringLength(256)]
        public string ImageUrl { get; set; }
        
        [IgnoreDataMember]
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
