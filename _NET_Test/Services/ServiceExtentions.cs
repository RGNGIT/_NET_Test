using _NET_Test.DatabaseModels;
using _NET_Test.Repositories;

namespace _NET_Test.Services
{

	public interface IUsersService
	{
		public Task<User> AddNew(UsersRepository repo, string username, string password);
		public Task<User?> FetchByUsername(UsersRepository repo, string username);
		public AuthUser JwtToUser(string jwtHeader);
	}

	public interface IActorsService
	{
		public Task<Actor?> Fetch(ActorsRepository repository, int id);
		public Task<List<Movie>> FetchMovies(ActorsRepository repository, int id);
		public Task<List<Rating>> FetchRatings(ActorsRepository repository, int id);
		public Task<Actor> AddNew(ActorsRepository repository, string name, string surname);
		public Task<Actor> AddRating(ActorsRepository actorsRepository, UsersRepository usersRepository, Rating rating, int UserId);
		public Task<Actor> AddMovie(ActorsRepository actorsRepository, MoviesRepository moviesRepository, ActorsMoviesRepository actorsMoviesRepository, int ActorId, int MovieId);
		public Task<List<Actor>> FetchAll(ActorsRepository actorsRepo);
	}

	public interface IMoviesService
	{
		public Task<Movie?> Fetch(MoviesRepository repository, int id);
		public Task<List<Actor>> FetchActors(MoviesRepository repository, int id);
		public Task<List<Rating>> FetchRatings(MoviesRepository repository, int id);
		public Task<List<Movie>> FetchAll(MoviesRepository moviesRepo);
		public Task<Movie> AddNew(MoviesRepository moviesRepo, string name);
		public Task<Movie> AddRating(MoviesRepository moviesRepository, UsersRepository usersRepository, Rating rating, int UserId);
		public Task<Movie> AddActor(ActorsRepository actorsRepository, MoviesRepository moviesRepository, ActorsMoviesRepository actorsMoviesRepository, int MovieId, int ActorId);
	}

	public interface IHashService 
	{
		public string CreateHash(string value);
	}

	public static class ServiceExtentions
	{
		public static void AddServices(this IServiceCollection collection)
		{
            collection.AddTransient<IUsersService, UsersService>();
			collection.AddTransient<IActorsService, ActorsService>();
			collection.AddTransient<IMoviesService, MoviesService>();
            collection.AddSingleton<IHashService, HashService>();
            collection.AddScoped<ActorsMoviesRepository>();
			collection.AddScoped<ActorsRepository>();
			collection.AddScoped<MoviesRepository>();
			collection.AddScoped<UsersRepository>();
		}
    }
}

