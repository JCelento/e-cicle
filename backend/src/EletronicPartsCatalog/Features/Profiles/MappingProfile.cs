using AutoMapper;

namespace EletronicPartsCatalog.Features.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Api.Domain.Person, Profile>(MemberList.None);
        }
    }
}
