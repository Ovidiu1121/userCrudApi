
using Microsoft.EntityFrameworkCore;
using UserCrudApi.Users.Model;

namespace UserCrudApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
