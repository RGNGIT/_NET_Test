using _NET_Test.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace _NET_Test.Repositories
{
    public class ActorsRepository
    {
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

        public List<Actor> FindManyByName(string name)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return db.Actors.Where(x => x.Name == name).ToList();
            }
        }

        public List<Actor> FindManyBySurname(string surname)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return db.Actors.Where(x => x.Surname == surname).ToList();
            }
        }

        public List<Actor> FindMany(string name, string surname)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return db.Actors.Where(x => x.Surname == surname && x.Name == name).ToList();
            }
        }

    }
}
