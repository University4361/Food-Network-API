using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.API
{
    [DataContract]
    public class OrderDetails : Order
    {
        [DataMember]
        public IEnumerable<OrderProduct> OrderProducts { get; set; }

        public OrderDetails(Order order)
        {
            ID = order.ID;
            Address = order.Address;
            Comment = order.Comment;
            DeliveryTime = order.DeliveryTime;
            Price = order.Price;
            OrderStatus = order.OrderStatus;
            Customer = order.Customer;
        }
    }
}
