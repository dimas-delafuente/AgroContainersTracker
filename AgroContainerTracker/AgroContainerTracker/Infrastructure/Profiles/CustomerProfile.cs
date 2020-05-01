using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<AddCustomerRequest, CustomerEntity>();
            CreateMap<CustomerEntity, Customer>();
        }
    }
}
