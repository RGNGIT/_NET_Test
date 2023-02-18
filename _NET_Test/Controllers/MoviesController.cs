using Microsoft.AspNetCore.Mvc;
using _NET_Test.Services;
using _NET_Test.DatabaseModels;
using Microsoft.Extensions.Caching.Memory;

namespace _NET_Test.Controllers
{
    
    public class MoviesController : ControllerBase
    {

        private readonly IMemoryCache _memoryCache;

        public MoviesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("/api/v2/[controller]/[action]/{id}")]
        public async Task<IResult> Find(MoviesService moviesService, int id)
        {
            try
            {
                if (_memoryCache.TryGetValue(id, out var result))
                {
                    return Results.Ok(result);
                }
                else
                {
                    Movie? movie = await moviesService.Fetch(id);
                    if (movie == null)
                    {
                        return Results.NotFound("Чет не нашел");
                    } 
                    else
                    {
                        movie.Ratings = await moviesService.FetchRatings(id);
                        List<Actor> actors = await moviesService.FetchActors(id);
                        var resolve = new { movie.Id, movie.Name, movie.Ratings, actors };
                        _memoryCache.Set(id, resolve, new MemoryCacheEntryOptions 
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                        });
                        return Results.Ok(resolve);
                    }
                }
            }
            catch (Exception ex) 
            {
                return Results.Problem(ex.Message);
            }
        }

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
