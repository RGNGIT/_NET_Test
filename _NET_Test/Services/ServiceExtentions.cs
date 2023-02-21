using _NET_Test.Repositories;

namespace _NET_Test.Services
{

	public interface IUsersService
	{
		public async Task<User> AddNew(UsersRepository repo, string username, string password);
		public async Task<User?> FetchByUsername(UsersRepository repo, string username);
		public AuthUser JwtToUser(string jwtHeader);
	}

	public interface IActorsService
	{
		public async Task<Actor?> Fetch(ActorsRepository repository, int id);
		public async Task<List<Movie>> FetchMovies(ActorsRepository repository, int id);
		public async Task<List<Rating>> FetchRatings(ActorsRepository repository, int id);
		public async Task<Actor> AddNew(ActorsRepository repository, string name, string surname);
		public async Task<Actor> AddRating(ActorsRepository actorsRepository, UsersRepository usersRepository, Rating rating, int UserId);
		public async Task<Actor> AddMovie(ActorsRepository actorsRepository, MoviesRepository moviesRepository, ActorsMoviesRepository actorsMoviesRepository, int ActorId, int MovieId);
		public async Task<List<Actor>> FetchAll(ActorsRepository actorsRepo);
	}

	public interface IMoviesService
	{
		public async Task<Movie?> Fetch(MoviesRepository repository, int id);
		public async Task<List<Actor>> FetchActors(MoviesRepository repository, int id);
		public async Task<List<Rating>> FetchRatings(MoviesRepository repository, int id);
		public async Task<List<Movie>> FetchAll(MoviesRepository moviesRepo);
		public async Task<Movie> AddNew(MoviesRepository moviesRepo, string name);
		public async Task<Movie> AddRating(MoviesRepository moviesRepository, UsersRepository usersRepository, Rating rating, int UserId);
		public async Task<Movie> AddActor(ActorsRepository actorsRepository, MoviesRepository moviesRepository, ActorsMoviesRepository actorsMoviesRepository, int MovieId, int ActorId);
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

