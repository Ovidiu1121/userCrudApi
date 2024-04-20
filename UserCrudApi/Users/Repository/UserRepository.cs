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

        public async Task<User> CreateUser(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
           return await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>x.Name.Equals(name));
        }

        public async Task<User> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);

            user.Name= request.Name ?? user.Name;
            user.Email=request.Email ?? user.Email;
            user.Role=request.Role ?? user.Role;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
