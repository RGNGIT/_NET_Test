using _NET_Test.DatabaseModels;
using _NET_Test.Services;
using Microsoft.AspNetCore.Mvc;

namespace _NET_Test.Controllers
{
    public class ActorsController : ControllerBase
    {
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IResult> Register(ActorsService actorsService, Actor actor)
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
