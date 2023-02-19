using _NET_Test.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace _NET_Test.Repositories
{
    public class MoviesRepository
    {

        public async Task<List<Actor>> FindActorsOfMovie(int MovieId)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                List<Actor> actorsList = new List<Actor>();
                var MovieIncludingActors = await db.Movies
                    .Include(movie => movie.Actors)
                    .ThenInclude(row => row.Actor)
                    .FirstAsync(movie => movie.Id == MovieId);
                var Actors = MovieIncludingActors.Actors.Select(row => row.Actor);
                foreach(var actor in Actors) 
                {
                    actorsList.Add(await db.Actors
                        .Include(r => r.Ratings)
                        .ThenInclude(r => r.user)
                        .Where(r => r.Id == actor.Id).FirstAsync());
                }
                return actorsList;
            }
        }

        public async Task<List<Rating>> FindRatings(int MovieId)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                var movie = await db.Movies
                    .Include(r => r.Ratings)
                    .ThenInclude(r => r.user)
                    .Where(r => r.Id == MovieId).FirstAsync();
                return movie.Ratings;
            }
        }

        public async Task<List<Movie>> FindAll()
        {
            using(DatabaseContext db = new(Config.configuration))
            {
                return await db.Movies
                    .Include(r => r.Ratings)
                    .ThenInclude(r => r.user).ToListAsync();
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
