using UserCrudApi.Dto;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Users.Repository.interfaces
{
    public interface IUserRepository
    {
        Task<ListUserDto> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByNameAsync(string name);
        Task<UserDto> CreateUser(CreateUserRequest request);
        Task<UserDto> UpdateUser(int id, UpdateUserRequest request);
        Task<UserDto> DeleteUserById(int id);

    }
}
