using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace _NET_Test.Services
{
	public static class ServiceExtentions
	{
		public static void AddServices(this IServiceCollection collection)
		{
            collection.AddTransient<UsersService>();
			collection.AddSingleton<HashService>();
		}
    }
}

