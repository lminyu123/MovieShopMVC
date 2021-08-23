using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Model;


namespace ApplicationCore.ServiceInterfaces

{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieCardResponseModel>> GetTopRatedMovies();
        Task<List<MovieDetailsResponseModel>> GetAllMovie();
        Task<IEnumerable<MovieReviewResponseModel>> GetMovieReviews(int id);




    }
}
