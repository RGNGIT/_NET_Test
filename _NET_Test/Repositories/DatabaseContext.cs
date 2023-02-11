using Microsoft.EntityFrameworkCore;
using _NET_Test.Classes;

namespace _NET_Test.Repositories
{
    public class DatabaseContext: DbContext
    {

        private IConfiguration configuration { get; }

        public DbSet<User>? Users { get; set; }

        public DatabaseContext(IConfiguration configuration) 
        {
            this.configuration = configuration;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Local"));
        }

    }
}
