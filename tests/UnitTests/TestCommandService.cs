using System.Threading.Tasks;
using Moq;
using tests.Helpers;
using UserCrudApi.Dto;
using UserCrudApi.System.Constant;
using UserCrudApi.System.Exceptions;
using UserCrudApi.Users.Repository.interfaces;
using UserCrudApi.Users.Service;
using UserCrudApi.Users.Service.interfaces;
using Xunit;

namespace tests.UnitTests;

public class TestCommandService
{
    Mock<IUserRepository> _mock;
    IUserCommandService _service;

    public TestCommandService()
    {
        _mock = new Mock<IUserRepository>();
        _service = new UserCommandService(_mock.Object);
    }
    
        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateUserRequest()
            {
                Name="Test",
                Email="test",
                Role="testrole"
            };

            var user = TestUserFactory.CreateUser(2);

            _mock.Setup(repo => repo.GetByNameAsync("Test")).ReturnsAsync(user);
                
           var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateUser(create));

            Assert.Equal(Constants.USER_ALREADY_EXIST, exception.Message);



        }

        [Fact]
        public async Task Create_ReturnUser()
        {

            var create = new CreateUserRequest()
            {
                Name="Test",
                Email="test",
                Role="testrole"
            };

            var user = TestUserFactory.CreateUser(2);
            user.Name=create.Name;
            user.Email=create.Email;
            user.Role=create.Role;

            _mock.Setup(repo => repo.CreateUser(It.IsAny<CreateUserRequest>())).ReturnsAsync(user);

            var result = await _service.CreateUser(create);

            Assert.NotNull(result);
            Assert.Equal(result, user);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateUserRequest()
            {
                Name="Test",
                Email="test",
                Role="testrole"
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((UserDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateUser(1, update));

            Assert.Equal(Constants.USER_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_InvalidData()
        {
            var update = new UpdateUserRequest()
            {
                Name="Test",
                Email="test",
                Role="testrole"
            };

            _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((UserDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateUser(5, update));

            Assert.Equal(Constants.USER_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateUserRequest()
            {
                Name="Test",
                Email="test",
                Role="testrole"
            };

            var user = TestUserFactory.CreateUser(2);
            
            user.Name=update.Name;
            user.Email=update.Email;
            user.Role=update.Role;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(user);
            _mock.Setup(repoo => repoo.UpdateUser(It.IsAny<int>(), It.IsAny<UpdateUserRequest>())).ReturnsAsync(user);

            var result = await _service.UpdateUser(5, update);

            Assert.NotNull(result);
            Assert.Equal(user, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeleteUserById(It.IsAny<int>())).ReturnsAsync((UserDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteUser(5));

            Assert.Equal(exception.Message, Constants.USER_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var user = TestUserFactory.CreateUser(2);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

            var result= await _service.DeleteUser(1);

            Assert.NotNull(result);
            Assert.Equal(user, result);


        }
    
    
}