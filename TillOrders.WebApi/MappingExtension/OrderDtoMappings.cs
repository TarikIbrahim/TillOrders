using TillOrders.Domain.Model;
using TillOrders.WebApi.AutoMapper;
using TillOrders.WebApi.Dtos.Order;

namespace TillOrders.WebApi.MappingExtension
{
    public static class OrderDtoMappings
    {
        public static OrderDto ToDto(this Order order)
        {
            return order.MapTo<Order, OrderDto>();
        }

        public static Order ToEntity(this OrderDto orderDto)
        {
            return orderDto.MapTo<OrderDto, Order>();
        }
    }
}
