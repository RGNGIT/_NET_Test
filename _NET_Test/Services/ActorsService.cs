using _NET_Test.Repositories;
using _NET_Test.DatabaseModels;

namespace _NET_Test.Services
{
    public class ActorsService: IActorsService
    {

        public async Task<Actor?> Fetch(ActorsRepository repository, int id)
        {
            return await repository.FindOneById(id);
        }

        public async Task<List<Movie>> FetchMovies(ActorsRepository repository, int id)
        {
            return await repository.FindMoviesOfActor(id);
        }

        public async Task<List<Rating>> FetchRatings(ActorsRepository repository, int id)
        {
            return await repository.FindRatings(id);
        }

        public async Task<Actor> AddNew(ActorsRepository repository, string name, string surname)
        {
            Actor actor = new() 
            {
                Name = name,
                Surname = surname
            };
            return await repository.AddOne(actor);
        }

        public async Task<Actor> AddRating(ActorsRepository actorsRepository, UsersRepository usersRepository, Rating rating, int UserId)
        {
            Actor? actor = await actorsRepository.FindOneById(rating.ProductId);
            User? user = await usersRepository.FindOneById(UserId);
            if (actor == null || user == null)
            {
                throw new Exception("Could not resolve Actor or User");
            }
            rating.user = user!;
            actor!.Ratings.Add(rating);
            return await actorsRepository.Refresh(actor!);
        }

        public async Task<Actor> AddMovie(ActorsRepository actorsRepository, MoviesRepository moviesRepository, ActorsMoviesRepository actorsMoviesRepository, int ActorId, int MovieId)
        {
            Movie? movie = await moviesRepository.FindOneById(MovieId);
            Actor? actor = await actorsRepository.FindOneById(ActorId);
            if (movie == null || actor == null)
            {
                throw new Exception("Could not resolve Movie or Actor");
            }
            await actorsMoviesRepository.Associate(actor.Id, movie.Id);
            return await actorsRepository.Refresh(actor!);
        }

        public async Task<List<Actor>> FetchAll(ActorsRepository actorsRepo) 
        {
            return await actorsRepo.FindAll();
        }
    }
}
