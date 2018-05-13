using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CoreWebAPI.Models
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryTime { get; set; }

        [DataMember]
        public float Price { get; set; }

        [DataMember]
        public Status OrderStatus { get; set; }

        [DataMember]
        [Required]
        public Address Address { get; set; }

        [IgnoreDataMember]
        [Required]
        public Courier Courier { get; set; }

        [DataMember]
        [Required]
        public Customer Customer { get; set; }

        [IgnoreDataMember]
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }

    public enum Status : int
    {
        New = 0,
        InProcess = 1,
        Completed = 2
    }
}
