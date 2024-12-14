using AutoMapper;
using UserApi.DTO_s;
using UserApi.Entities;

namespace UserApi.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDataDTO, UserData>();
            CreateMap<UserData, UserDataDTO>(); ;
        }
    }
}
