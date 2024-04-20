using UserCrudApi.System.Constant;
using UserCrudApi.System.Exceptions;
using UserCrudApi.Users.Model;
using UserCrudApi.Users.Repository.interfaces;
using UserCrudApi.Users.Service.interfaces;

namespace UserCrudApi.Users.Service
{
    public class UserQueryService: IUserQueryService
    {
        private IUserRepository _repository;

        public UserQueryService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> users = await _repository.GetAllAsync();

            if (users.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_USERS_EXIST);
            }

            return users;
        }

        public async Task<User> GetById(int id)
        {
            User user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            }

            return user;
        }

        public async Task<User> GetByName(string name)
        {

            User user = await _repository.GetByNameAsync(name);

            if (user == null)
            {
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            }

            return user;
        }
    }
}
