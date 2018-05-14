using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebAPI.EF;
using CoreWebAPI.Models;
using CoreWebAPI.Models.Request;
using CoreWebAPI.Models.API;
using CoreWebAPI.Models.Response;

namespace CoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Orders
        [HttpPost]
        public IActionResult GetOrders([FromBody] GetOrdersRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                IEnumerable<Order> orders;

                if (request.OrdersDate != default(DateTime))
                {
                    orders = _context.Orders
                    .Include(o => o.Address)
                    .Include(o => o.Customer)
                    .Where(order => order.Courier.ID == courierToken.CourierID && order.DeliveryTime.Date == request.OrdersDate.Date);
                }
                else
                {
                    orders = _context.Orders
                    .Include(o => o.Address)
                    .Include(o => o.Customer)
                    .Where(order => order.Courier.ID == courierToken.CourierID);
                }

                return Ok(orders);
            }
            else
                return BadRequest(ModelState);
        }

        // POST: api/Orders/GetOrder
        [HttpPost("GetOrder")]
        public IActionResult GetOrder([FromBody] GetOrderRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                Order order = _context.Orders
                    .Include(o => o.Address)
                    .Include(o => o.Customer)
                    .Include(o => o.ProductOrders)
                        .ThenInclude(o => o.Product)
                    .AsNoTracking()
                    .FirstOrDefault(o => o.ID == request.OrderID);

                List<OrderProduct> orderProducts = new List<OrderProduct>();

                foreach (var po in order.ProductOrders)
                {
                    orderProducts.Add(new OrderProduct(po.Product) { Quantity = po.Quantity });
                }

                OrderDetails orderDetails = new OrderDetails(order)
                {
                    OrderProducts = orderProducts
                };

                return Ok(orderDetails);
            }
            else
                return BadRequest(ModelState);
        }

        // POST: api/Orders/UpdateStatus
        [HttpPost("UpdateStatus")]
        public IActionResult UpdateStatus([FromBody] UpdateOrderStatusRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                Order order = _context.Orders.FirstOrDefault(o => o.ID == request.OrderID);
                order.OrderStatus = request.NewStatus;
                _context.Update(order);

                _context.SaveChanges();

                return Ok(ModelState);
            }
            else
                return BadRequest(ModelState);
        }

        // POST: api/Orders/VerifyOrder
        [HttpPost("VerifyOrder")]
        public IActionResult VerifyOrder([FromBody] VerifyOrderRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                Order order = _context.Orders
                    .Include(o => o.Address)
                    .Include(o => o.Customer)
                    .Include(o => o.ProductOrders)
                        .ThenInclude(o => o.Product)
                    .AsNoTracking().FirstOrDefault(o => o.ID == request.Order.ID);

                List<ProductOrder> productOrders = _context.ProductOrders
                    .Include(po => po.OrderID).Where(po => po.OrderID == request.Order.ID ).ToList();

                foreach (var op in request.Order.OrderProducts)
                {
                    ProductOrder productOrder = productOrders.FirstOrDefault(prodOrd => prodOrd.ProductID == op.ID);
                    productOrder.Quantity = op.Quantity;

                    _context.Update(productOrder);
                }

                order.Price = request.Order.Price;

                _context.Update(order);

                _context.SaveChanges();

                return Ok(request.Order);
            }
            else
                return BadRequest(ModelState);
        }

        // POST: api/Orders/GetOrdersHistory
        [HttpPost("GetOrdersHistory")]
        public IActionResult GetOrdersHistory([FromBody] GetOrdersHistoryRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                GetOrdersHistoryResponse response = new GetOrdersHistoryResponse();

                List<OrderHistory> histories = new List<OrderHistory>();

                List<Order> orders = _context.Orders
                    .Include(o => o.Courier)
                    .AsNoTracking()
                    .Where(o => o.Courier.ID == courierToken.CourierID && o.DeliveryTime < request.ToDate && o.DeliveryTime > request.FromDate).ToList();

                DateTime dateTime = request.FromDate;

                while (dateTime.Date <= request.ToDate.Date)
                {
                    Report report = _context.Reports
                        .Include(r => r.Courier)
                        .FirstOrDefault(rep => rep.Courier.ID == courierToken.CourierID && rep.ReportDate.Date == dateTime.Date);

                    List<Order> dateOrders = orders.Where(ord => ord.DeliveryTime.Date == dateTime.Date).ToList();

                    if (dateOrders != null && dateOrders.Any())
                    {
                        histories.Add(
                            new OrderHistory
                            {
                                HistoryDate = dateTime,
                                NumberOfCompletedOrders = dateOrders.Where(ord => ord.OrderStatus == Status.Completed).Count(),
                                NumberOfCanceledOrders = dateOrders.Where(ord => ord.OrderStatus == Status.Canceled).Count(),
                                DateProfit = report?.Profit ?? 0,
                                Distance = report?.Distance ?? 0
                            });
                    }

                    dateTime = dateTime.AddDays(1);
                }

                response.OrdersHistories = histories;

                return Ok(response);
            }
            else
                return BadRequest(ModelState);
        }

        private bool CheckToken(BaseRequest request, out CourierToken courierToken)
        {
            courierToken = _context.CourierTokens.FirstOrDefault(token => token.Value == request.Token);

            if (courierToken != null && courierToken.DateOfExpire > DateTime.Now)
                return true;
            else
            {
                ModelState.AddModelError("error", "Токен недействителен");
                return false;
            }
        }
    }
}