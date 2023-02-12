using _NET_Test.Repositories;
using _NET_Test.DatabaseModels;

namespace _NET_Test.Services
{
	public class UsersService
	{

		public async Task<User> AddNew(string username, string password)
		{
			User user = new() 
			{ 
				Username = username, 
				Password = password 
			};
			UsersRepository repo = new();
			User? check = await repo.FindOneByUsername(username);
			if(check == null) 
			{
                return await repo.AddOne(user);
            }
			else
			{
				throw new Exception("User with this username already exists!");
			}
		}

	}
}

