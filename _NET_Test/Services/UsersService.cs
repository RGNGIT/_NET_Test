using _NET_Test.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using _NET_Test.Classes;
namespace _NET_Test.Services
{
	public class UsersService
	{

		public async Task<User> Add(string username, string password)
		{
			User user = new() 
			{ 
				Username = username, 
				Password = password 
			};
			UsersRepository repo = new();
			await repo.AddOne(user);
			return user;
		}

	}
}

