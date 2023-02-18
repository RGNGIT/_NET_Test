using _NET_Test.DatabaseModels;
using _NET_Test.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace _NET_Test.Controllers
{
    public class ActorsController : ControllerBase
    {

        record ActorResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public List<Rating> Ratings { get; set; }
            public List<Movie> Movies { get; set; }
        }

        private readonly IMemoryCache _memoryCache;

        public ActorsController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("/api/v2/[controller]/[action]/{id}")]
        public async Task<IResult> Find(ActorsService actorsService, int id)
        {
            try
            {
                if (_memoryCache.TryGetValue(id, out var result))
                {
                    return Results.Ok(result);
                }
                else
                {
                    Actor? actor = await actorsService.Fetch(id);
                    if (actor == null)
                    {
                        return Results.NotFound("Чет не нашел");
                    }
                    else
                    {
                        List<Movie> movies = await actorsService.FetchMovies(id);
                        actor.Ratings = await actorsService.FetchRatings(id);
                        ActorResponse resolve = new ActorResponse() 
                        {
                            Id = actor.Id,
                            Name = actor.Name,
                            Surname = actor.Surname,
                            Ratings = actor.Ratings,
                            Movies = movies
                        };
                        _memoryCache.Set($"Actor.{id}", resolve, new MemoryCacheEntryOptions
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
        public async Task<IResult> ShowAll(ActorsService actorsService)
        {
            try
            {
                return Results.Ok(await actorsService.FetchAll());
            } 
            catch(Exception ex) 
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> AddActor(ActorsService actorsService, Actor actor)
        {
            try
            {
                string Name = actor.Name;
                string Surname = actor.Surname;
                return Results.Ok(await actorsService.AddNew(Name, Surname));
            }
            catch(Exception ex) 
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
