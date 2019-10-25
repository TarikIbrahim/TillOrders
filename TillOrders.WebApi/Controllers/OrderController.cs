using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TillOrders.Services;
using TillOrders.WebApi.Dtos.Order;

namespace TillOrders.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> GetOrderById([FromRoute]int id)
        {
            var order = _orderService.GetOrderById(id);
            return new OrderDto()
            {
                id = order.Id,
                OrderName = order.OrderName
            };
        }

        [HttpPost(Name ="create")]
        public async Task PlaceOrder(OrderDto order)
        {
            if (order == null)
                throw new NullReferenceException();

           // _orderService.InsertOrder()


        }
    }
}