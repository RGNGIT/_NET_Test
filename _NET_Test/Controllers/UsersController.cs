using _NET_Test.Services;
using Microsoft.AspNetCore.Mvc;
using _NET_Test.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

		[HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> Login(UsersService userService, HashService hashService, User user)
		{
			try
			{
				User? userFound = await userService.FetchByUsername(user.Username!);
				if(userFound != null && userFound.Password == hashService.CreateHash(user.Password!)) 
				{
                    List<Claim> claims = new List<Claim> 
					{ 
						new Claim("Id", userFound.Id!.ToString()),
						new Claim("Username", userFound.Username!) 
					};
                    JwtSecurityToken jwt = new (
						issuer: Config.JWT.Issuer,
						audience: Config.JWT.Audience,
                        claims: claims,
						expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(3000)),
						signingCredentials: new SigningCredentials(Config.JWT.GetSecretKey(), SecurityAlgorithms.HmacSha256)
						);
                    return Results.Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
                } 
				else
				{
                    return Results.Problem("Wrong username or password");
                }
            }
			catch (Exception ex) 
			{
                return Results.Problem(ex.Message);
            }
		}
	}
}

