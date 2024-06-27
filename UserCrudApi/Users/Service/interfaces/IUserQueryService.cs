using UserCrudApi.Dto;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Users.Service.interfaces
{
    public interface IUserQueryService
    {
        Task<ListUserDto> GetAllUsers();
        Task<UserDto> GetByName(string name);
        Task<UserDto> GetById(int id);
    }
}
