using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<AddSupplierRequest, SupplierEntity>();
            CreateMap<SupplierEntity, Supplier>();
        }
    }
}