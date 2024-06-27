using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using tests.Helpers;
using UserCrudApi.Dto;
using UserCrudApi.System.Constant;
using UserCrudApi.System.Exceptions;
using UserCrudApi.Users.Controllers;
using UserCrudApi.Users.Controllers.interfaces;
using UserCrudApi.Users.Service.interfaces;
using Xunit;

namespace tests.UnitTests;

public class TestController
{
    
    Mock<IUserCommandService> _command;
    Mock<IUserQueryService> _query;
    UserApiController _controller;

    public TestController()
    {
        _command = new Mock<IUserCommandService>();
        _query = new Mock<IUserQueryService>();
        _controller = new UserController(_command.Object, _query.Object);
    }
    
        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAllUsers()).ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));
           
            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.USER_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var users = TestUserFactory.CreateUsers(5);

            _query.Setup(repo => repo.GetAllUsers()).ReturnsAsync(users);

            var result = await _controller.GetAll();
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var userssAll = Assert.IsType<ListUserDto>(okresult.Value);

            Assert.Equal(5, userssAll.userList.Count);
            Assert.Equal(200, okresult.StatusCode);


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

                _command.Setup(repo => repo.CreateUser(It.IsAny<CreateUserRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.USER_ALREADY_EXIST));

                var result = await _controller.CreateUser(create);

                var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

                Assert.Equal(400,bad.StatusCode);
                Assert.Equal(Constants.USER_ALREADY_EXIST, bad.Value);

            }

            [Fact]
            public async Task Create_ValidData()
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

                _command.Setup(repo => repo.CreateUser(create)).ReturnsAsync(user);

                var result = await _controller.CreateUser(create);

                var okResult= Assert.IsType<CreatedResult>(result.Result);

                Assert.Equal(okResult.StatusCode, 201);
                Assert.Equal(user, okResult.Value);

            }

            [Fact]
            public async Task Update_InvalidDate()
            {

                var update = new UpdateUserRequest()
                {
                    Name="Test",
                    Email="test",
                    Role="testrole"
                };

                _command.Setup(repo => repo.UpdateUser(11, update)).ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));

                var result = await _controller.UpdateUser(11, update);

                var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

                Assert.Equal(bad.StatusCode, 404);
                Assert.Equal(bad.Value, Constants.USER_DOES_NOT_EXIST);

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

            _command.Setup(repo=>repo.UpdateUser(5,update)).ReturnsAsync(user);

            var result = await _controller.UpdateUser(5, update);

            var okResult=Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, user);

        }


        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo=>repo.DeleteUser(2)).ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));

            var result= await _controller.DeleteUser(2);

            var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.USER_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var user = TestUserFactory.CreateUser(2);

            _command.Setup(repo => repo.DeleteUser(2)).ReturnsAsync(user);

            var result = await _controller.DeleteUser(2);

            var okResult=Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(user, okResult.Value);

        }
    
    
}