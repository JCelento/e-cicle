using AutoMapper;

namespace EletronicPartsCatalog.Features.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Api.Domain.Person, User>(MemberList.None);
        }
    }
}
