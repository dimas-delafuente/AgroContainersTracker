using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.ProductManagement;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles.ProductManagement
{
    public class ProductWeighingProfile : Profile
    {
        public ProductWeighingProfile()
        {
            CreateMap<ProductWeighing, ProductWeighingEntity>().ReverseMap();

            CreateMap<AddProductWeighingRequest, ProductWeighingEntity>()
                .ForMember(dest => dest.ProductRecords, opt => opt.Ignore());
        }

    }

    
}
