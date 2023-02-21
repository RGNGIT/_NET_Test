using _NET_Test.DatabaseModels;
using _NET_Test.Repositories;

namespace _NET_Test.Services
{
    public class MoviesService: IMoviesService
    {

        public async Task<Movie?> Fetch(MoviesRepository repository, int id)
        {
            return await repository.FindOneById(id);
        }

        public async Task<List<Actor>> FetchActors(MoviesRepository repository, int id)
        {
            return await repository.FindActorsOfMovie(id);
        }

        public async Task<List<Rating>> FetchRatings(MoviesRepository repository, int id)
        {
            return await repository.FindRatings(id);
        }

        public async Task<List<Movie>> FetchAll(MoviesRepository moviesRepo)
        {
            return await moviesRepo.FindAll();
        }

        public async Task<Movie> AddNew(MoviesRepository moviesRepo, string name)
        {
            Movie movie = new() 
            {
                Name = name
            };
            return await moviesRepo.AddOne(movie);
        }

        public async Task<Movie> AddRating(MoviesRepository moviesRepository, UsersRepository usersRepository, Rating rating, int UserId)
        {
            Movie? movie = await moviesRepository.FindOneById(rating.ProductId);
            User? user = await usersRepository.FindOneById(UserId);
            if (movie == null || user == null) 
            {
                throw new Exception("Could not resolve Movie or User");
            }
            rating.user = user!;
            movie!.Ratings.Add(rating);
            return await moviesRepository.Refresh(movie!);
        }

        public async Task<Movie> AddActor(ActorsRepository actorsRepository, MoviesRepository moviesRepository, ActorsMoviesRepository actorsMoviesRepository, int MovieId, int ActorId)
        {
            Movie? movie = await moviesRepository.FindOneById(MovieId);
            Actor? actor = await actorsRepository.FindOneById(ActorId);
            if(movie == null || actor == null) 
            {
                throw new Exception("Could not resolve Movie or Actor");
            }
            await actorsMoviesRepository.Associate(actor.Id, movie.Id);
            return await moviesRepository.Refresh(movie!);
        }
    }
}
