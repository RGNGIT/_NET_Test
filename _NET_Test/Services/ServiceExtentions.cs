using _NET_Test.Repositories;

namespace _NET_Test.Services
{
	public static class ServiceExtentions
	{
		public static void AddServices(this IServiceCollection collection)
		{
            collection.AddTransient<UsersService>();
			collection.AddTransient<ActorsService>();
			collection.AddTransient<MoviesService>();
            collection.AddSingleton<HashService>();
            collection.AddScoped<ActorsMoviesRepository>();
			collection.AddScoped<ActorsRepository>();
			collection.AddScoped<MoviesRepository>();
			collection.AddScoped<UsersRepository>();
		}
    }
}

