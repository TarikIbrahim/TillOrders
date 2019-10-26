﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TillOrders.Services;
using TillOrders.WebApi.Dtos.Order;
using TillOrders.WebApi.MappingExtension;

namespace TillOrders.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/order")]
    public class OrderController : ControllerBase
    {
       
        #region Fields
        private readonly IOrderService _orderService;
        #endregion

        #region Constructor
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        [HttpGet("{id}")]
        public async Task<OrderDto> GetOrderById([FromRoute]int id)
        {
            var order = _orderService.GetOrderById(id);
            return order.ToDto();

        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<OkObjectResult> PlaceOrder(OrderDto order)
        {
            if (order == null)
                throw new NullReferenceException();

            var item = order.ToEntity();
            var orderitems = order.OrderItems.Select(oi => oi.ToEntity()).ToList();

            orderitems.ForEach(x => _orderService.CreateOrderItem(x));
            _orderService.InsertOrder(item);

            return Ok(order);

        }
    }
}