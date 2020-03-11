using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AutoMapper;
using System.Collections.Generic;


namespace AgroContainerTracker.Profiles
{
    public class ContainerProfile : Profile
    {
        public ContainerProfile()
        {
            CreateMap<AddContainerRequest, Container>()
                .ForMember(dest => dest.Palots, opt => opt.MapFrom(_ => new List<Palot>()));

            CreateMap<ContainerEntity, Container>()
                .ForMember(dest => dest.Palots, opt => opt.MapFrom(src => src.Palots));
        }
    }
}
