using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.Request
{
    public class GetOrdersRequest : BaseRequest
    {
        public DateTime OrdersDate { get; set; }
    }
}
