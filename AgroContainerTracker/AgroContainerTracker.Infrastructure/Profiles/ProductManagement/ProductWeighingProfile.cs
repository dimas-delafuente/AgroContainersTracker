using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.ProductManagement;
using AutoMapper;
using System.Collections.Generic;

namespace AgroContainerTracker.Infrastructure.Profiles.ProductManagement
{
    public class ProductWeighingProfile : Profile
    {
        public ProductWeighingProfile()
        {
            CreateMap<ProductWeighing, ProductWeighingEntity>().ReverseMap();

            CreateMap<AddProductWeighingRequest, ProductWeighingEntity>()
                .ForMember(dest => dest.ProductRecords, opt => opt.MapFrom(x => GetProductRecords(x)));
        }

        private IEnumerable<ProductRecordEntity> GetProductRecords(AddProductWeighingRequest productWeighing)
        {
            List<ProductRecordEntity> productRecords = new List<ProductRecordEntity>();

            foreach(var productPkg in productWeighing.ProductRecords)
            {
                double productGrossWeigh = productWeighing.GrossWeight / productPkg.Quantity;
                double productTareWeight = productWeighing.TareWeight / productPkg.Quantity;
                double productNetWeight = productWeighing.NetWeight / productPkg.Quantity;

                for(int i = 0; i < productPkg.Quantity; i++)
                {
                    productRecords.Add(new ProductRecordEntity
                    {
                        CampaingId = productWeighing.CampaingId,
                        ProductEntryNumber = productWeighing.ProductEntryNumber,
                        BuyerId = productWeighing.BuyerId,
                        SellerId = productWeighing.SellerId,
                        ColdRoomId = productWeighing.ColdRoomId,
                        FruitId = productWeighing.FruitId,
                        PackagingId = productPkg.Packaging.PackagingId,
                        IsOwnPackaging = productPkg.IsOwnPackaging,
                        GrossWeight = productGrossWeigh,
                        TareWeight = productTareWeight,
                        NetWeight = productNetWeight
                    });
                }
            }

            return productRecords;
        }
    }

    
}
