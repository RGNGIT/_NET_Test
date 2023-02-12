using _NET_Test.Repositories;
using _NET_Test.DatabaseModels;

namespace _NET_Test.Services
{
    public class ActorsService
    {
        public async Task<Actor> AddNew(string name, string surname)
        {
            Actor actor = new() 
            {
                Name = name,
                Surname = surname
            };
            ActorsRepository repo = new();
            return await repo.AddOne(actor);
        }
    }
}
