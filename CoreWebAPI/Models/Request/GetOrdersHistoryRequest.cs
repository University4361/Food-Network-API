using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.Request
{
    public class GetOrdersHistoryRequest : BaseRequest
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
