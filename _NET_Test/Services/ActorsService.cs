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

        public async Task<Actor> AddRating(Rating rating, int UserId)
        {
            ActorsRepository actorsRepo = new();
            UsersRepository usersRepository = new();
            Actor? actor = await actorsRepo.FindOneById(rating.ProductId);
            User? user = await usersRepository.FindOneById(UserId);
            if (actor == null || user == null)
            {
                throw new Exception("Some shit");
            }
            rating.user = user!;
            actor!.Ratings.Add(rating);
            return await actorsRepo.Refresh(actor!);
        }

        public async Task<List<Actor>> FetchAll() 
        {
            ActorsRepository actorsRepo = new();
            return await actorsRepo.FindAll();
        }
    }
}
