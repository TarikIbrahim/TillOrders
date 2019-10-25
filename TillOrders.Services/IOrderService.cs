using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TillOrders.Domain.Model;

namespace TillOrders.Services
{
    public partial interface IOrderService
    {
        #region Order
        IList<Order> GetAll();
        Order GetOrderById(int id);
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        #endregion

        #region OrderItems
        IList<OrderItem> GetByOrderId(int id);
        void CreateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(OrderItem orderItem);
        
        #endregion

    }
}