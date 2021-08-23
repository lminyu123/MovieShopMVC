using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel model);
        Task<UserLoginResponseModel> Login(LoginRequestModel model);
        Task<IEnumerable<MovieCardResponseModel>> GetPurchaseMovie(int UserId);
        Task<IEnumerable<MovieCardResponseModel>> GetFavoriate(int UserId);
        Task<IEnumerable<AccountResponseModel>> GetAccount();
        Task<AccountResponseModel> GetAccountById(int id);

        Task<PurchaseMovieResponseModel> PurchaseMovie(PurchaseMovieModel purchaseMovie);
        Task<List<PurchaseMovieModel>> GetPurchaseById(int id);
        Task<FavoriteResponseModel> AddToFavorite(FavoriteRequestModel model);
        Task<UnFavoriteResponseModel> removefromFavorite(UnFavoriteRequestModel model);
        Task<List<FavoriteRequestModel>> GetFavoriteById(int id);
        Task<MovieCardResponseModel> GetFavoriteMovieDetail(int id, int movieId);
        Task<List<MovieReviewsModel>> GetReviews(int id);
        Task<ReviewsResponseModel> PostReviews(ReviewsRequestModel model);
        Task<ReviewsResponseModel> PutReviews(ReviewsRequestModel model);
        Task<string> DeleteReviews(int id, int movieId);

    }
}

    