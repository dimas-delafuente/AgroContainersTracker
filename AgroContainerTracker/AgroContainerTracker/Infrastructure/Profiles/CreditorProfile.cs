using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class CreditorProfile : Profile
    {
        public CreditorProfile()
        {
            CreateMap<AddCreditorRequest, CreditorEntity>();
            CreateMap<CreditorEntity, Creditor>();
        }
    }
}