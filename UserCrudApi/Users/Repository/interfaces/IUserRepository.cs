using UserCrudApi.Dto;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Users.Repository.interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task<User> CreateUser(CreateUserRequest request);
        Task<User> UpdateUser(int id, UpdateUserRequest request);
        Task<User> DeleteUserById(int id);

    }
}
