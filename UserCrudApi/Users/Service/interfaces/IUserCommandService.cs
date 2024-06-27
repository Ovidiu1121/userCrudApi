using UserCrudApi.Dto;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Users.Service.interfaces
{
    public interface IUserCommandService
    {
        Task<UserDto> CreateUser(CreateUserRequest request);
        Task<UserDto> UpdateUser(int id, UpdateUserRequest request);
        Task<UserDto> DeleteUser(int id);
    }
}
