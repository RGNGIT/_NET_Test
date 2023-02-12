using Microsoft.EntityFrameworkCore;
using _NET_Test.DatabaseModels;

namespace _NET_Test.Repositories
{
    public class DatabaseContext: DbContext
    {

        private IConfiguration configuration { get; }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Actor> Actors { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;

        public DatabaseContext(IConfiguration configuration) 
        {
            this.configuration = configuration;
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Local"));
            optionsBuilder.LogTo(Console.WriteLine);
        }

    }
}
