using CoreWebAPI.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.Request
{
    public class VerifyOrderRequest : BaseRequest
    {
        public OrderDetails Order { get; set; }
    }
}
