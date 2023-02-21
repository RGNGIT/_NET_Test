using Microsoft.AspNetCore.Mvc;
using _NET_Test.Services;
using _NET_Test.DatabaseModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using _NET_Test.Repositories;

namespace _NET_Test.Controllers
{
    
    public class MoviesController : ControllerBase
    {

        record MovieResponse
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public List<Rating>? Ratings { get; set; }
            public List<Actor>? Actors { get; set; }
        }

        private readonly IMemoryCache _memoryCache;

        public MoviesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("/api/v2/[controller]/[action]/{id}")]
        public async Task<IResult> Find(IMoviesService moviesService, MoviesRepository moviesRepository, int id)
        {
            try
            {
                if (_memoryCache.TryGetValue(id, out var result))
                {
                    return Results.Ok(result);
                }
                else
                {
                    Movie? movie = await moviesService.Fetch(moviesRepository, id);
                    if (movie == null)
                    {
                        return Results.NotFound("Could not resolve Movie");
                    } 
                    else
                    {
                        movie.Ratings = await moviesService.FetchRatings(moviesRepository, id);
                        List<Actor> actors = await moviesService.FetchActors(moviesRepository, id);
                        MovieResponse resolve = new MovieResponse()
                        {
                            Id = movie.Id,
                            Name = movie.Name,
                            Ratings = movie.Ratings,
                            Actors = actors
                        };
                        _memoryCache.Set($"Movie.{id}", resolve, new MemoryCacheEntryOptions 
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
        public async Task<IResult> ShowAll(IMoviesService moviesService, MoviesRepository moviesRepository)
        {
            try
            {
                if (_memoryCache.TryGetValue("CachedMoviesList", out var result))
                {
                    return Results.Ok(result);
                }
                else
                {
                    List<Movie> movies = await moviesService.FetchAll(moviesRepository);
                    _memoryCache.Set("CachedMoviesList", movies, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                    });
                    return Results.Ok(movies);
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddMovie(IMoviesService movieService, MoviesRepository moviesRepository, Movie movie)
        {
            try
            {
                return Results.Ok(await movieService.AddNew(moviesRepository, movie.Name));
            } 
            catch (Exception ex) 
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddCastActor(IMoviesService movieService, MoviesRepository moviesRepository, ActorsRepository actorsRepository, ActorsMoviesRepository actorsMoviesRepository, Actor actor, Movie movie)
        {
            try
            {
                return Results.Ok(await movieService.AddActor(actorsRepository, moviesRepository, actorsMoviesRepository, movie.Id, actor.Id));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IResult> Popular(IMoviesService moviesService, MoviesRepository moviesRepository)
        {
            try
            {
                List<Movie> movies = await moviesService.FetchAll(moviesRepository);
                movies.Sort();
                return Results.Ok(movies);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
