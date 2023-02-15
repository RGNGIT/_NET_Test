using Microsoft.AspNetCore.Mvc;
using _NET_Test.Services;
using _NET_Test.DatabaseModels;

namespace _NET_Test.Controllers
{
    public class MoviesController : ControllerBase
    {

        [HttpGet]
        public async Task<IResult> ShowAll(MoviesService moviesService)
        {
            try
            {
                return Results.Ok(await moviesService.FetchAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddMovie(MoviesService movieService, Movie movie)
        {
            try
            {
                return Results.Ok(await movieService.AddNew(movie.Name));
            } 
            catch (Exception ex) 
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddCastActor(MoviesService movieService, Actor actor, Movie movie)
        {
            try
            {
                return Results.Ok(await movieService.AddActor(movie.Id, actor.Id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
