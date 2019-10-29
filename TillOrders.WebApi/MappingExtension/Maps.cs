using AutoMapper;
using TillOrders.Domain.Model;
using TillOrders.WebApi.AutoMapper;
using TillOrders.WebApi.Dtos.Order;
using System.Linq;
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

            AutoMapperApiConfiguration.MapperConfigurationExpression.CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.OrderItemId, y => y.MapFrom(src => src.Id))
                .ForMember(x => x.Order, option => option.Ignore());

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            AutoMapperApiConfiguration.MapperConfigurationExpression.CreateMap<Order, OrderDto>()
                .ForMember(x => x.OrderId, y => y.MapFrom(src => src.Id))
                .ForMember(x => x.OrderItems, y => y.MapFrom(src => src.OrderItems.Select(x => x.ToDto())));

            
        }
    }
}
