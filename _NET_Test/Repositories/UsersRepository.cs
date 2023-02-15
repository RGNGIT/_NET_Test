using _NET_Test.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace _NET_Test.Repositories
{
	public class UsersRepository
	{

		public async Task<User> AddOne(User user) 
		{
			using (DatabaseContext db = new (Config.configuration))
			{
				await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
				return user;
            }
		}

        public async Task<User?> FindOneById(int id)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                return await db.Users.FindAsync(id); ;
            }
        }

        public async Task<User?> FindOneByUsername(string username)
		{
			using (DatabaseContext db = new (Config.configuration))
			{
				return await db.Users.SingleOrDefaultAsync(x => x.Username == username);
            }
		}

	}
}

