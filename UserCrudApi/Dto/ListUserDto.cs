namespace UserCrudApi.Dto;

public class ListUserDto
{
    public ListUserDto()
    {
        userList = new List<UserDto>();
    }
    
    public List<UserDto> userList { get; set; }
}