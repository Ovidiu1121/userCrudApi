using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserCrudApi.Data;
using UserCrudApi.Dto;
using UserCrudApi.Users.Model;
using UserCrudApi.Users.Repository.interfaces;

namespace UserCrudApi.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<UserDto> CreateUser(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> DeleteUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<ListUserDto> GetAllAsync()
        {
            List<User> result = await _context.Users.ToListAsync();
            
            ListUserDto listUserDto = new ListUserDto()
            {
                userList = _mapper.Map<List<UserDto>>(result)
            };

            return listUserDto;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByNameAsync(string name)
        {
            var user = await _context.Users.Where(u => u.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);

            user.Name= request.Name ?? user.Name;
            user.Email=request.Email ?? user.Email;
            user.Role=request.Role ?? user.Role;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}
