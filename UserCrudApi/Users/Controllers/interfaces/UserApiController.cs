using Microsoft.AspNetCore.Mvc;
using UserCrudApi.Dto;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Users.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class UserApiController:ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<User>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListUserDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(User))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(User))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(User))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<UserDto>> DeleteUser([FromRoute] int id);

        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(User))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<UserDto>> GetByIdRoute([FromRoute] int id);
        
        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(User))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<UserDto>> GetByNameRoute([FromRoute] string name);
    }
}
