using _NET_Test.Repositories;
using _NET_Test.DatabaseModels;

namespace _NET_Test.Services
{
    public class ActorsService
    {

        public async Task<Actor?> Fetch(int id)
        {
            ActorsRepository repository = new();
            return await repository.FindOneById(id);
        }

        public async Task<List<Movie>> FetchMovies(int id)
        {
            ActorsRepository repository = new();
            return await repository.FindMoviesOfActor(id);
        }

        public async Task<List<Rating>> FetchRatings(int id)
        {
            ActorsRepository repository = new();
            return await repository.FindRatings(id);
        }

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

        public async Task<Actor> AddMovie(int ActorId, int MovieId)
        {
            ActorsMoviesRepository actorsMoviesRepository = new();
            MoviesRepository moviesRepository = new();
            ActorsRepository actorsRepository = new();
            Movie? movie = await moviesRepository.FindOneById(MovieId);
            Actor? actor = await actorsRepository.FindOneById(ActorId);
            if (movie == null || actor == null)
            {
                throw new Exception("Some shit");
            }
            await actorsMoviesRepository.Associate(actor.Id, movie.Id);
            return await actorsRepository.Refresh(actor!);
        }

        public async Task<List<Actor>> FetchAll() 
        {
            ActorsRepository actorsRepo = new();
            return await actorsRepo.FindAll();
        }
    }
}
