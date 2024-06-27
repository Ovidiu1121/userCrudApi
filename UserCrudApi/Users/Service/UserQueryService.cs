using UserCrudApi.Dto;
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

        public async Task<ListUserDto> GetAllUsers()
        {
            ListUserDto users = await _repository.GetAllAsync();

            if (users.userList.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_USERS_EXIST);
            }

            return users;
        }

        public async Task<UserDto> GetById(int id)
        {
            UserDto user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            }

            return user;
        }

        public async Task<UserDto> GetByName(string name)
        {

            UserDto user = await _repository.GetByNameAsync(name);

            if (user == null)
            {
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            }

            return user;
        }
    }
}
