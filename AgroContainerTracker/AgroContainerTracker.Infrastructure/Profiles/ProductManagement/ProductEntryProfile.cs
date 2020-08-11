using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.ProductManagement;
using AutoMapper;
using System.Linq;

namespace AgroContainerTracker.Infrastructure.Profiles.ProductManagement
{
    public class ProductEntryProfile : Profile
    {
        public ProductEntryProfile()
        {
            CreateMap<ProductEntry, ProductEntryEntity>()
                .ForMember(dest => dest.Sellers, opt => opt.MapFrom(src => src.Sellers.Select(x => new ProductEntrySellerEntity
                {
                    CampaingId = src.CampaingId,
                    ProductEntryNumber = src.ProductEntryNumber,
                    CustomerId = x.CustomerId
                })))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.CustomerId))
                .ForMember(dest => dest.Buyer, opt => opt.Ignore())
                .ForMember(dest => dest.PayerId, opt => opt.MapFrom(src => src.Payer.CustomerId))
                .ForMember(dest => dest.Payer, opt => opt.Ignore());


            CreateMap<ProductEntryEntity, ProductEntry>()
                .ForMember(dest => dest.Sellers, opt => opt.MapFrom(src => src.Sellers.Select(x => x.Customer)));
        }
    }
}
