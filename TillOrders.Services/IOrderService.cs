using System.Collections.Generic;
using TillOrders.Domain.Model;

namespace TillOrders.Services
{
    public partial interface IOrderService
    {
        #region Order
        IList<Order> GetAll(OrderPaymentStatus status);
        Order GetOrderById(int id);
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        void MarkAsPaid(Order order);
        #endregion

        #region OrderItems
        IList<OrderItem> GetOrderItemByOrderId(int orderId);
        void CreateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(OrderItem orderItem);

        #endregion

    }
}