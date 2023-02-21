using _NET_Test.DatabaseModels;
using _NET_Test.Repositories;
using _NET_Test.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _NET_Test.Controllers
{
    public class RatingsController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddRatingToMovie(IUsersService userService, IMoviesService moviesService, MoviesRepository moviesRepository, UsersRepository usersRepository, Rating rating)
        {
            try
            {
                AuthUser user = userService.JwtToUser(Request.Headers["Authorization"]!);
                return Results.Ok(await moviesService.AddRating(moviesRepository, usersRepository, rating, user.Id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddRatingToActor(IUsersService userService, IActorsService actorsService, ActorsRepository actorsRepository, UsersRepository usersRepository, Rating rating)
        {
            try
            {
                AuthUser user = userService.JwtToUser(Request.Headers["Authorization"]!);
                return Results.Ok(await actorsService.AddRating(actorsRepository, usersRepository, rating, user.Id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
