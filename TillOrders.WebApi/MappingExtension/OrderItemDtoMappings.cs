using TillOrders.Domain.Model;
using TillOrders.WebApi.AutoMapper;
using TillOrders.WebApi.Dtos.Order;

namespace TillOrders.WebApi.MappingExtension
{
    public static class OrderItemDtoMappings
    {
        public static OrderItemDto ToDto(this OrderItem orderItem)
        {
            return orderItem.MapTo<OrderItem, OrderItemDto>();
        }

        public static OrderItem ToEntity(this OrderItemDto orderItemDto)
        {
            return orderItemDto.MapTo<OrderItemDto, OrderItem>();
        }
    }
}
