using UserCrudApi.Dto;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Users.Service.interfaces
{
    public interface IUserCommandService
    {
        Task<User> CreateUser(CreateUserRequest request);
        Task<User> UpdateUser(int id, UpdateUserRequest request);
        Task<User> DeleteUser(int id);
    }
}
