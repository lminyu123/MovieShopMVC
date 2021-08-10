using System;
using System.Collections.Generic;
using ApplicationCore.Model;


namespace ApplicationCore.ServiceInterfaces

{
    public interface IMovieService
    {
        List<MovieCardResponseModel> GotTopRevenueMovies();
    }
}
