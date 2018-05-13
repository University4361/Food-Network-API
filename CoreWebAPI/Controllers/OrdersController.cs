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
        public IActionResult GetOrders([FromBody] BaseRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                IEnumerable<Order> orders = _context.Order
                    .Include(o => o.Address)
                    .Include(o => o.Customer)
                    .Where(order => order.Courier.ID == courierToken.CourierID); 

                return Ok(orders);
            }
            else
                return BadRequest(ModelState);
        }

        // POST: api/Orders
        [HttpPost("GetOrder")]
        public IActionResult GetOrder([FromBody] GetOrderRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                Order order = _context.Order
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

        // POST: api/Orders
        [HttpPost("UpdateStatus")]
        public IActionResult UpdateStatus([FromBody] UpdateOrderStatusRequest request)
        {
            if (CheckToken(request, out CourierToken courierToken))
            {
                Order order = _context.Order.FirstOrDefault(o => o.ID == request.OrderID);
                order.OrderStatus = request.NewStatus;
                _context.Update(order);

                _context.SaveChanges();

                return Ok(ModelState);
            }
            else
                return BadRequest(ModelState);
        }

        private bool CheckToken(BaseRequest request, out CourierToken courierToken)
        {
            courierToken = _context.CourierToken.FirstOrDefault(token => token.Value == request.Token);

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