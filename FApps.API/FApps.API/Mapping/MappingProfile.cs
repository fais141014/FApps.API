using AutoMapper;
using FApps.Core.Domain;
using FApps.Core.DTO;

namespace FApps.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
        }
    }
}
