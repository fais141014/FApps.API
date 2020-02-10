using AutoMapper;
using FApps.Core.Domain;
using FApps.Core.DTO;

namespace FApps.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }

}
