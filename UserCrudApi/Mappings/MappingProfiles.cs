using AutoMapper;
using UserCrudApi.Dto;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
