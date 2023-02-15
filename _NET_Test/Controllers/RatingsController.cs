using _NET_Test.DatabaseModels;
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
        public async Task<IResult> AddRatingToMovie(UsersService userService, MoviesService moviesService, Rating rating)
        {
            try
            {
                AuthUser user = userService.JwtToUser(Request.Headers["Authorization"]!);
                return Results.Ok(await moviesService.AddRating(rating, user.Id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddRatingToActor(UsersService userService, ActorsService actorsService, Rating rating)
        {
            try
            {
                AuthUser user = userService.JwtToUser(Request.Headers["Authorization"]!);
                return Results.Ok(await actorsService.AddRating(rating, user.Id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
