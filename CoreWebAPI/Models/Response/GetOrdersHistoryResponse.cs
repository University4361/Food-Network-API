using CoreWebAPI.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.Response
{
    public class GetOrdersHistoryResponse
    {
        public List<OrderHistory> OrdersHistories { get; set; }
    }
}
