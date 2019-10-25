using System.Collections.Generic;

namespace TillOrders.Domain.Model
{
    public partial class Order : BaseEntity
    {
        private ICollection<OrderItem> _orderItems;

        public string OrderName { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }

        public virtual ICollection<OrderItem> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new List<OrderItem>()); }
            protected set { _orderItems = value; }
        }
    }
}
