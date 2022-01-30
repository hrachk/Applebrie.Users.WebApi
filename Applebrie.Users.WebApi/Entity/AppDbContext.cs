using Applebrie.Users.WebApi.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Applebrie.Users.WebApi.Entity
{
    public class AppDbContext:DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // database provider is configured before runtime migration update is applied e.g:
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
        //    Database.Migrate();
        //}
    }
}
