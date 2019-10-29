using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        //TODO: DI Logger,for real-world use NLog nuget package
        //TODO: Creating custom response including user friendly error message

        #region Fields
        private readonly IOrderService _orderService;
        private readonly ILogger logger;
        #endregion

        #region Constructor
        public OrderController(IOrderService orderService, ILogger<OrderController> _logger)
        {
            _orderService = orderService;
            logger = _logger;
        }

        #endregion

        #region Actions

        [HttpGet("{id}")]
        public async Task<OrderDto> GetOrderById([FromRoute]int id)
        {
            if (id <= 0)
            {
                Log(LogLevel.Error,"Invalid order id.");

                return new OrderDto();
            }

            Log(LogLevel.Debug, string.Format("'{0}' has been invoked", nameof(GetOrderById)));

            var order = _orderService.GetOrderById(id);

            Log(LogLevel.Information, "The order has been retrieved successfully.");

            return order.ToDto();
        }

        [HttpPost("create")]
        public virtual IActionResult PlaceOrder(OrderDto order)
        {
            if (order == null)
            {
                Log(LogLevel.Error,"Null order object");

                return new BadRequestResult();
            }

            var item = order.ToEntity();
            var orderitems = order.OrderItems.Select(oi => oi.ToEntity()).ToList();

            //Calculate order amount based on order items prices and quantity
            decimal orderAmount = orderitems.Sum(x => x.Price * x.Quantity);
            item.Amount = orderAmount;
            _orderService.InsertOrder(item);

            Log(LogLevel.Information, "The order has been placed successfully.");

            return new JsonResult(item.ToDto());

        }

        [HttpPost("confirm")]
        public virtual IActionResult ConfirmOrder(int orderId)
        {
            if (orderId <= 0)
                return new BadRequestResult();

            var currentOrder = _orderService.GetOrderById(orderId);

            if (currentOrder != null)
            {
                currentOrder.IsPaid = true;
                _orderService.UpdateOrder(currentOrder);

                Log(LogLevel.Information, "The order has been updated successfully.");
            }

            Log(LogLevel.Error, "The order has not been updated,it's null object.");

            return new JsonResult(currentOrder.ToDto());
        }

        [HttpGet()]
        [Route("~/api/v1/orders/all")]
        public virtual IActionResult GetAllOrders(OrderPaymentStatus status)
        {
            var orders = _orderService.GetAll(status);

            var dtos = orders.ToList().Select(x => x.ToDto()).ToList();
            return new JsonResult(dtos);

        }

        [HttpPost()]
        [Route("~/api/v1/order/{orderId:int}/items")]
        public virtual IActionResult GetOrderItems(int orderId)
        {
            var orderItems = _orderService.GetOrderItemByOrderId(orderId);

            var dtos = orderItems.ToList().Select(x => x.ToDto()).ToList();
            return new JsonResult(dtos);

        }


        [HttpDelete()]
        [Route("~/api/v1/order/delete/{orderId:int}")]
        public virtual IActionResult DeleteOrder(int orderId)
        {
            var target_order = _orderService.GetOrderById(orderId);
            if (target_order != null)
            {
                //Copy
                var xOrderItems = target_order.OrderItems;

                _orderService.DeleteOrder(target_order);
                Log(LogLevel.Information, "The order has been deleted successfully.");

                xOrderItems.ToList().ForEach(x => _orderService.DeleteOrderItem(x));
                Log(LogLevel.Information, "The order items have been deleted successfully.");
            }

            return new OkResult();
        }

        [HttpDelete()]
        [Route("~/api/v1/order/{orderId:int}/items/{orderItemId:int}")]
        public virtual IActionResult DeleteOrderItem(int orderId, int orderItemId)
        {
            var orderItems = _orderService.GetOrderItemByOrderId(orderId);
            var targetOrderItem = orderItems.Where(x => x.Id == orderItemId).SingleOrDefault();

            if (targetOrderItem != null)
            {
                _orderService.DeleteOrderItem(targetOrderItem);
                Log(LogLevel.Information, "Order item has been deleted successfully");
            }

            Log(LogLevel.Error, "Order item could not been deleted");

            return new OkResult();

        }

        #endregion

        #region Utilities
        private void Log(LogLevel logType, string message)
        {
            switch (logType)
            {
                case LogLevel.Information:
                    logger?.LogInformation(message);
                    break;

                case LogLevel.Warning:
                    logger?.LogWarning(message, null);
                    break;

                case LogLevel.Error:
                    logger?.LogError(message, null);
                    break;

                case LogLevel.Critical:
                    logger?.LogCritical(message);
                    break;

                default:
                    logger?.LogDebug(message, null);
                    break;
            }

        }

        #endregion

    }
}