using AutoMapper;
using TillOrders.Domain.Model;
using TillOrders.WebApi.AutoMapper;
using TillOrders.WebApi.Dtos.Order;

namespace TillOrders.WebApi.MappingExtension
{
    public static class Maps
    {
        public static IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            return AutoMapperApiConfiguration.MapperConfigurationExpression.CreateMap<TSource, TDestination>().IgnoreAllNonExisting();
        }

        public static void CreateAllMappings()
        {
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}
