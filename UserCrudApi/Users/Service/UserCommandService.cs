
using UserCrudApi.Dto;
using UserCrudApi.System.Constant;
using UserCrudApi.System.Exceptions;
using UserCrudApi.Users.Model;
using UserCrudApi.Users.Repository.interfaces;
using UserCrudApi.Users.Service.interfaces;

namespace UserCrudApi.Users.Service
{
    public class UserCommandService: IUserCommandService
    {
        private IUserRepository _repository;

        public UserCommandService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> CreateUser(CreateUserRequest request)
        {
            UserDto user= await _repository.GetByNameAsync(request.Name);

            if (user!=null)
            {
                throw new ItemAlreadyExists(Constants.USER_ALREADY_EXIST);
            }

            user=await _repository.CreateUser(request);
            return user;
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            UserDto user = await _repository.GetByIdAsync(id);

            if (user==null)
            {
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            }

            await _repository.DeleteUserById(id);
            return user;
        }

        public async Task<UserDto> UpdateUser(int id, UpdateUserRequest request)
        {
            UserDto user = await _repository.GetByIdAsync(id);

            if (user==null)
            {
                throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
            }

            user = await _repository.UpdateUser(id, request);
            return user;
        }
    }
}
