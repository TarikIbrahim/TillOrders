using System.Collections.Generic;

namespace TillOrders.WebApi.Dtos.Order
{
    public class OrderDto
    {
        private ICollection<OrderItemDto> _orderItems;

        public int OrderId { get; set; }

        public string OrderName { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }

        public virtual ICollection<OrderItemDto> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new List<OrderItemDto>()); }
            protected set { _orderItems = value; }
        }
    }
}