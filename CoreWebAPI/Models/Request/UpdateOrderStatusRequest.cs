using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.Request
{
    public class UpdateOrderStatusRequest : BaseRequest
    {
        public int OrderID { get; set; }

        public Status NewStatus { get; set; }
    }
}
