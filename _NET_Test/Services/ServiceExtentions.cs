namespace _NET_Test.Services
{
	public static class ServiceExtentions
	{
		public static void AddServices(this IServiceCollection collection)
		{
            collection.AddTransient<UsersService>();
			collection.AddTransient<ActorsService>();
			collection.AddSingleton<HashService>();
			collection.AddSingleton<MoviesService>();
		}
    }
}

