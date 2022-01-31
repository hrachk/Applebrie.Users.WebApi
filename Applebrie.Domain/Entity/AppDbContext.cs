

using Microsoft.EntityFrameworkCore;

namespace Applebrie.Domain
{
    public class AppDbContext:DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

       
    }
}
