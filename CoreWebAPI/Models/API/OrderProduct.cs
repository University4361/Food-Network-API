using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.API
{
    [DataContract]
    public class OrderProduct : Product
    {
        [DataMember]
        public int Quantity { get; set; }

        public OrderProduct(Product product)
        {
            ID = product.ID;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            ImageUrl = product.ImageUrl;
        }
    }
}
