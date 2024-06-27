using Microsoft.AspNetCore.Mvc;
using UserCrudApi.Dto;
using UserCrudApi.System.Exceptions;
using UserCrudApi.Users.Controllers.interfaces;
using UserCrudApi.Users.Model;
using UserCrudApi.Users.Repository.interfaces;
using UserCrudApi.Users.Service.interfaces;

namespace UserCrudApi.Users.Controllers
{
    public class UserController: UserApiController
    {

        private IUserCommandService _userCommandService;
        private IUserQueryService _userQueryService;

        public UserController(IUserCommandService userCommandService, IUserQueryService userQueryService)
        {
           _userCommandService = userCommandService;
            _userQueryService = userQueryService;
        }

        public override async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var users = await _userCommandService.CreateUser(request);

                return Created("Userul a fost adaugat",users);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<UserDto>> DeleteUser([FromRoute] int id)
        {
            try
            {
                var users = await _userCommandService.DeleteUser(id);

                return Accepted("", users);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<UserDto>> GetByIdRoute(int id)
        {
            try
            {
                var users = await _userQueryService.GetById(id);
                return Ok(users);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListUserDto>> GetAll()
        {
            try
            {
                var users = await _userQueryService.GetAllUsers();
                return Ok(users);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<UserDto>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var users = await _userQueryService.GetByName(name);
                return Ok(users);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var users = await _userCommandService.UpdateUser(id, request);

                return Ok(users);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
