using _NET_Test.Repositories;
using _NET_Test.DatabaseModels;
using System.Text.Json;
using System.Reflection;

namespace _NET_Test.Services
{

    public class UsersService: IUsersService
	{

		public async Task<User> AddNew(UsersRepository repo, string username, string password)
		{
			User user = new() 
			{ 
				Username = username, 
				Password = password 
			};
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

		public async Task<User?> FetchByUsername(UsersRepository repo, string username)
		{
			return await repo.FindOneByUsername(username);
		}

		public AuthUser JwtToUser(string jwtHeader)
		{
            string[] jwtSplit = jwtHeader.ToString().Split(' ');
			jwtHeader = Config.JWT.Decode(jwtSplit[1]);
			string[] jsonSplit = jwtHeader.Split('.');
            AuthUser user = JsonSerializer.Deserialize<AuthUser>(jsonSplit[1])!;
			return user;
		}
	}
}

