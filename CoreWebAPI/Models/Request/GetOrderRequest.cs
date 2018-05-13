using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.Request
{
    public class GetOrderRequest : BaseRequest
    {
        public int OrderID { get; set; }
    }
}
