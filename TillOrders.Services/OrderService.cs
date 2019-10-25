using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TillOrders.Domain.Data;
using TillOrders.Domain.Model;

namespace TillOrders.Services
{
    public  partial class OrderService:IOrderService
    {
        #region Fields
        private readonly IRepository<OrderItem> _orderItemRepo;
        private readonly IRepository<Order> _orderRepo;
        #endregion

        #region Constructor
        public OrderService(IRepository<Order>orderRepo,IRepository<OrderItem>orderItemRepo)
        {
            _orderItemRepo = orderItemRepo;
            _orderRepo = orderRepo;
        }
        #endregion

        #region Methods
        public virtual void CreateOrderItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            _orderItemRepo.Delete(orderItem);
        }

        public virtual IList<Order> GetAll()
        {
            return _orderRepo.TableNoTracking.ToList();
        }

        public virtual IList<OrderItem> GetByOrderId(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Order GetOrderById(int id)
        {
            if (id == 0)
                return null;

            return _orderRepo.GetById(id);
        }

        public virtual void InsertOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orderRepo.Insert(order);
        }

        public virtual void UpdateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orderRepo.Update(order);
        }

        #endregion

    }
}
