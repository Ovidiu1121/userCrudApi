using UserCrudApi.Dto;

namespace tests.Helpers;

public class TestUserFactory
{
    public static UserDto CreateUser(int id)
    {
        return new UserDto
        {
            Id = id,
            Name="Bob"+id,
            Email="testemail@yahoo"+id,
            Role="tester"+id
        };
    }

    public static ListUserDto CreateUsers(int count)
    {
        ListUserDto users=new ListUserDto();
            
        for(int i = 0; i<count; i++)
        {
            users.userList.Add(CreateUser(i));
        }
        return users;
    }
}