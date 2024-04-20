using UserCrudApi.Users.Model;

namespace UserCrudApi.Users.Service.interfaces
{
    public interface IUserQueryService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetByName(string name);
        Task<User> GetById(int id);
    }
}
