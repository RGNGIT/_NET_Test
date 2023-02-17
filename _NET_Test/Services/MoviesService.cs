﻿using _NET_Test.DatabaseModels;
using _NET_Test.Repositories;

namespace _NET_Test.Services
{
    public class MoviesService
    {

        public async Task<List<Movie>> FetchAll()
        {
            MoviesRepository moviesRepo = new();
            return await moviesRepo.FindAll();
        }

        public async Task<Movie> AddNew(string name)
        {
            MoviesRepository moviesRepo = new();
            Movie movie = new() 
            {
                Name = name
            };
            return await moviesRepo.AddOne(movie);
        }

        public async Task<Movie> AddRating(Rating rating, int UserId)
        {
            MoviesRepository moviesRepository = new();
            UsersRepository usersRepository= new();
            Movie? movie = await moviesRepository.FindOneById(rating.ProductId);
            User? user = await usersRepository.FindOneById(UserId);
            if (movie == null || user == null) 
            {
                throw new Exception("Some shit");
            }
            rating.user = user!;
            movie!.Ratings.Add(rating);
            return await moviesRepository.Refresh(movie!);
        }

        public async Task<Movie> AddActor(int MovieId, int ActorId)
        {
            MoviesRepository moviesRepository = new();
            ActorsRepository actorsRepository = new();
            Movie? movie = await moviesRepository.FindOneById(MovieId);
            Actor? actor = await actorsRepository.FindOneById(ActorId);
            if(movie == null || actor == null) 
            {
                throw new Exception("Some shit");
            }
            return await moviesRepository.Refresh(movie!);
        }
    }
}