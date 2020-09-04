using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.ProductManagement;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles.ProductManagement
{
    public class ProductRecordProfile : Profile
    {
        public ProductRecordProfile()
        {
            CreateMap<ProductRecordEntity, ProductRecord>().ReverseMap();
        }
    }
}
