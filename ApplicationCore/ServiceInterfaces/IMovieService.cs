using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Model;


namespace ApplicationCore.ServiceInterfaces

{
    public interface IMovieService
    {
       Task <List<MovieCardResponseModel>> GotTopRevenueMovies();

    }
}
