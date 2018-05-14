using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.API
{
    public class OrderHistory
    {
        public DateTime HistoryDate { get; set; }

        public int NumberOfCompletedOrders { get; set; }

        public int NumberOfCanceledOrders { get; set; }

        public double DateProfit { get; set; }

        public double Distance { get; set; }
    }
}
