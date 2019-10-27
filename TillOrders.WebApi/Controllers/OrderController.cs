using Microsoft.AspNetCore.Mvc;
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

        //TODO: DI Logger

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

        [HttpPost("create")]
        public Task<OrderDto> PlaceOrder(OrderDto order)
        {
            if (order == null)
                throw new NullReferenceException();

            var item = order.ToEntity();
            var orderitems = order.OrderItems.Select(oi => oi.ToEntity()).ToList();

            decimal orderAmount = orderitems.Sum(x => x.Price * x.Quantity);
            item.Amount = orderAmount;
            _orderService.InsertOrder(item);
            
            return Task.FromResult(item.ToDto());

        }

        [HttpPost("confirm")]
        public virtual IActionResult ConfirmOrder(int orderId)
        {
            if (orderId <= 0)
                return new BadRequestResult();

            var currentOrder = _orderService.GetOrderById(orderId);

            if (currentOrder!= null)
            {
                currentOrder.IsPaid = true;
                _orderService.UpdateOrder(currentOrder);
            }

            return new JsonResult(currentOrder.ToDto());
        }

        [HttpGet("all")]
        [Route("~/api/v1/orders/all")]
        public virtual IActionResult GetAllOrders(OrderPaymentStatus status)
        {
            var orders = _orderService.GetAll(status);

            var dtos = orders.ToList().Select(x => x.ToDto()).ToList();
            return new JsonResult(dtos);

        }

        [HttpPost("items")]
        [Route("~/api/v1/order/{orderId:int}/items")]
        public virtual IActionResult GetOrderItems(int orderId)
        {
            var orderItems = _orderService.GetOrderItemByOrderId(orderId);
            
            var dtos= orderItems.ToList().Select(x => x.ToDto()).ToList();
            return new JsonResult(dtos);

        }

        
        [HttpPost("delete")]
        [Route("~/api/v1/order/delete/{orderId:int}")]
        public virtual IActionResult DeleteOrder(int orderId)
        {
            var orderItems = _orderService.GetOrderItemByOrderId(orderId);
            var target_order = _orderService.GetOrderById(orderId);
            if (target_order != null)
            {
                _orderService.DeleteOrder(target_order);
                orderItems.ToList().ForEach(x => _orderService.DeleteOrderItem(x));
            }

            return new OkResult();
        }

        [HttpPost("deleteitem")]
        [Route("~/api/v1/order/{orderId:int}/items/{orderItemId:int}")]
        public virtual IActionResult DeleteOrderItem(int orderId,int orderItemId)
        {
            var orderItems = _orderService.GetOrderItemByOrderId(orderId);
            var targetOrderItem = orderItems.Where(x => x.Id == orderItemId).SingleOrDefault();

            if (targetOrderItem != null)
                _orderService.DeleteOrderItem(targetOrderItem);

            return new OkResult();

        }
    }
}