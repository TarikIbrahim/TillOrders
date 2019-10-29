using System;
using System.Collections.Generic;
using System.Linq;
using TillOrders.Domain.Data;
using TillOrders.Domain.Model;

namespace TillOrders.Services
{
    public enum OrderPaymentStatus { all=1, paid=2, notpaid=3 }
    public partial class OrderService : IOrderService
    {
        #region Fields
        private readonly IRepository<OrderItem> _orderItemRepo;
        private readonly IRepository<Order> _orderRepo;
        #endregion

        #region Constructor
        public OrderService(IRepository<Order> orderRepo, IRepository<OrderItem> orderItemRepo)
        {
            _orderItemRepo = orderItemRepo;
            _orderRepo = orderRepo;
        }
        #endregion

        #region Methods
        public virtual void CreateOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            _orderItemRepo.Insert(orderItem);
        }

        public virtual void DeleteOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orderRepo.Delete(order);
        }

        public virtual void DeleteOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            _orderItemRepo.Delete(orderItem);
        }

        public virtual IList<Order> GetAll(OrderPaymentStatus status)
        {
            var OrdersQuery = _orderRepo.TableNoTracking;

            switch (status)
            {
                case OrderPaymentStatus.paid:
                    OrdersQuery = OrdersQuery.Where(o => o.IsPaid == true);
                    break;

                case OrderPaymentStatus.notpaid:
                    OrdersQuery = OrdersQuery.Where(o => o.IsPaid == false);
                    break;

                default:
                    break;
            }
            
            return OrdersQuery.ToList();

        }

        public virtual IList<OrderItem> GetOrderItemByOrderId(int orderId)
        {
            return _orderItemRepo.Table.Where(o => o.OrderId == orderId).ToList();
        }

        public virtual Order GetOrderById(int id)
        {
            if (id == 0)
                return null;
            
            var mOrder = _orderRepo.GetById(id);
            mOrder.OrderItems.ToList().AddRange(_orderItemRepo.Table.Where(o => o.OrderId == id));
            return mOrder;
        }

        public virtual void InsertOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            order.IsPaid = false;
            _orderRepo.Insert(order);
        }

        public virtual void UpdateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orderRepo.Update(order);
        }

        public void MarkAsPaid(Order order)
        {
            if (order == null)
                throw new ArgumentNullException();

            order.IsPaid = true;
            _orderRepo.Update(order);
        }

        #endregion

    }
}
