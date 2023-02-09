using System;
namespace _NET_Test.Services
{
	public static class ServiceExtentions
	{
		public static void AddUserService(this IServiceCollection collection) => collection.AddTransient<UserService>();
	}
}

