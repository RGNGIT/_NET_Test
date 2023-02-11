using System;
using _NET_Test.Classes;
namespace _NET_Test.Repositories
{
	public class UsersRepository
	{

		public async Task AddOne(User user) 
		{
			using(DatabaseContext db = new(Config.configuration!))
			{
				await db.Users!.AddAsync(user);
                await db.SaveChangesAsync();
            }
		}

	}
}

