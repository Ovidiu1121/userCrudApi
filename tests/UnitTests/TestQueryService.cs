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

public class TestQueryService
{
    Mock<IUserRepository> _mock;
    IUserQueryService _service;

    public TestQueryService()
    {
        _mock=new Mock<IUserRepository>();
        _service=new UserQueryService(_mock.Object);
    }
    
    
        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListUserDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllUsers());

            Assert.Equal(exception.Message, Constants.NO_USERS_EXIST);       

        }

        [Fact]
        public async Task GetAll_ReturnAllUsers()
        {

            var users = TestUserFactory.CreateUsers(5);

            _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(users);

            var result = await _service.GetAllUsers();

            Assert.NotNull(result);
            Assert.Contains(users.userList[1], result.userList);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((UserDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

            Assert.Equal(Constants.USER_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnDUser()
        {

            var user = TestUserFactory.CreateUser(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(user);

            var result = await _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(user, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((UserDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

            Assert.Equal(Constants.USER_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByName_ReturnUser()
        {

            var user=TestUserFactory.CreateUser(5);
            user.Name="test";

            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(user);

            var result = await _service.GetByName("test");

            Assert.NotNull(result);
            Assert.Equal(user, result);

        }   
    
    
    
    
}