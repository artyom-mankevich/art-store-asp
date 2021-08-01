using AutoMapper;
using MyWebApp.Data.Entities;
using MyWebApp.ViewModels;

namespace MyWebApp.Data
{
    public class DutchMappingProfile : Profile 
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, 
                    ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }
    }
}