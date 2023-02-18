using _NET_Test.DatabaseModels;

namespace _NET_Test.Repositories
{
    public class ActorsMoviesRepository
    {
        public async Task Associate(int ActorId, int MovieId)
        {
            using (DatabaseContext db = new(Config.configuration))
            {
                await db.AddAsync(new ActorMovie
                {
                    ActorId = ActorId,
                    MovieId = MovieId
                });
            }
        }
    }
}
