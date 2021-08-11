﻿using System;
using System.Collections.Generic;
using ApplicationCore.Model;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieServiceTest : IMovieService
    {

        public List<MovieCardResponseModel> GotTopRevenueMovies()
        {
            var movies = new List<MovieCardResponseModel>()
            {
                   new MovieCardResponseModel {Id = 1, Title = "Avengers: Infinity War"},
                   new MovieCardResponseModel {Id = 2, Title = "Avatar"},
                   new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens"},
                   new MovieCardResponseModel {Id = 4, Title = "Titanic"},
                   new MovieCardResponseModel {Id = 5, Title = "Inception"},
                   new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron"},
                   new MovieCardResponseModel {Id = 7, Title = "Interstellar"},
                   new MovieCardResponseModel {Id = 8, Title = "Fight Club"},
                   new MovieCardResponseModel {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring" }


            };

            return movies;
        }
    }
}
