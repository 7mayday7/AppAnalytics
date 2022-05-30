using Microsoft.EntityFrameworkCore;

namespace AppAnalytics.Models
{
    public class UsersContext
    {
        public class UserContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public UserContext(DbContextOptions<UserContext> options)
                : base(options)
            {
                Database.EnsureCreated();
            }
        }
    }
}
