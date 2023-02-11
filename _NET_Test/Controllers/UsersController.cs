using _NET_Test.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using _NET_Test.Classes;

namespace _NET_Test.Controllers
{

    public class UsersController: Controller
	{
		[HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddNew(UsersService userService, HashService hashService, User user)
		{
			try
			{
				string Username = user.Username!;
				string Password = hashService.CreateHash(user.Password!);
                User result = await userService.Add(Username, Password);
                return Results.Ok(user);
            }
			catch (Exception ex) 
			{
				return Results.Problem(ex.Message);
			}
		}

	}
}

