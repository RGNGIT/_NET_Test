using _NET_Test.Services;
using Microsoft.AspNetCore.Mvc;
using _NET_Test.DatabaseModels;

namespace _NET_Test.Controllers
{

    public class UsersController: ControllerBase
	{
		[HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> Register(UsersService userService, HashService hashService, User user)
		{
			try
			{
				string Username = user.Username!;
				string Password = hashService.CreateHash(user.Password!);
                return Results.Ok(await userService.AddNew(Username, Password));
            }
			catch (Exception ex) 
			{
				return Results.Problem(ex.Message);
			}
		}



	}
}

