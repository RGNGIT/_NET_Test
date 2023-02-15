using _NET_Test.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace _NET_Test.Repositories
{
    public class MoviesRepository
    {

        public async Task<List<Movie>> FindAll()
        {
            using(DatabaseContext db = new(Config.configuration))
            {
                return await db.Movies.ToListAsync();
            }
        }

        public async Task<Movie> AddOne(Movie movie)
        {
            using(DatabaseContext db = new(Config.configuration))
            {
                await db.Movies.AddAsync(movie);
                await db.SaveChangesAsync();
                return movie;
            }
        }

        public async Task<Movie?> FindOneById(int id)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return await db.Movies.FindAsync(id);
            }
        }

        public async Task<Movie?> FindOneByName(string name)
        {
            using(DatabaseContext db = new(Config.configuration))
            {
                return await db.Movies.SingleOrDefaultAsync(x => x.Name == name);
            }
        }

        public async Task<Movie> Refresh(Movie movie)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                db.Movies.Update(movie);
                await db.SaveChangesAsync();
                return movie;
            }
        }
    }
}
