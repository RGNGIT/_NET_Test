using _NET_Test.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace _NET_Test.Repositories
{
    public class ActorsRepository
    {

        public async Task<List<Movie>> FindMoviesOfActor(int ActorId)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                var ActorsIncludingMovies = await db.Actors.Include(actor => actor.Movies).ThenInclude(row => row.Movie).FirstAsync(actor => actor.Id == ActorId);
                var Movies = ActorsIncludingMovies.Movies.Select(row => row.Movie);
                return Movies.ToList();
            }
        }

        public async Task<List<Rating>> FindRatings(int ActorId)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return await db.Ratings.FromSqlRaw($"SELECT * FROM Ratings WHERE ActorId = {ActorId};").ToListAsync();
            }
        }

        public async Task<List<Actor>> FindAll()
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return await db.Actors.ToListAsync();
            }
        }

        public async Task<Actor> AddOne(Actor actor)
        {
            using(DatabaseContext db = new(Config.configuration))
            {
                await db.Actors.AddAsync(actor);
                await db.SaveChangesAsync();
                return actor;
            }
        }

        public async Task<Actor?> FindOneById(int id)
        {
            using(DatabaseContext db = new(Config.configuration))
            {
                return await db.Actors.FindAsync(id);
            }
        }

        public async Task<List<Actor>> FindManyByName(string name)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return await db.Actors.Where(x => x.Name == name).ToListAsync();
            }
        }

        public async Task<List<Actor>> FindManyBySurname(string surname)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return await db.Actors.Where(x => x.Surname == surname).ToListAsync();
            }
        }

        public async Task<List<Actor>> FindMany(string name, string surname)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return await db.Actors.Where(x => x.Surname == surname && x.Name == name).ToListAsync();
            }
        }

        public async Task<Actor> Refresh(Actor actor)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                db.Actors.Update(actor);
                await db.SaveChangesAsync();
                return actor;
            }
        }

        public async Task AssociateWithMovie(int ActorId, int MovieId)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                await db.AddAsync(new ActorMovie 
                {
                    ActorId = ActorId,
                    MovieId = MovieId
                });
            }
        }
    }
}
