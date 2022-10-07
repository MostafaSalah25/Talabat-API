using AutoMapper;
using Talabat.API.Dtos;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Identity;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>() 
                .ForMember(D => D.ProductType, o => o.MapFrom(S => S.ProductType.Name))
               .ForMember(D => D.ProductBrand, o => o.MapFrom(S => S.ProductBrand.Name))
               // PictureUrl
               .ForMember(D => D.PictureUrl, o => o.MapFrom<PictureUrlResolver>() );


            CreateMap<  DAL.Entities.Identity.Address, AddressDto>().ReverseMap(); 
            CreateMap<CustomerBasketDto, CustomerBasket>(); 
            CreateMap<BasketItemDto, BasketItem>();
            // Order > map to Add of Order not user ,use in Endpoint CreateOrder
            CreateMap<AddressDto, DAL.Entities.Order_Aggregate.Address >();

            
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, O => O.MapFrom(S => S.ItemOrdered.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(S => S.ItemOrdered.ProductName))
                //.ForMember(d => d.PictureUrl, O => O.MapFrom(S => S.ItemOrdered.PictureUrl));
                .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemUrlResolver>());  


        }

    }
}
