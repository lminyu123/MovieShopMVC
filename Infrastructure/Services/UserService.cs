using ApplicationCore.Exceptions;
using ApplicationCore.Model;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<FavoriteResponseModel> AddToFavorite(FavoriteRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteReviews(int id, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountResponseModel>> GetAccount()
        {
            var users = await _userRepository.ListAllAsync();
            var userList = new List<AccountResponseModel>();
            foreach (var user in users)
            {
                userList.Add(new AccountResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                    
                });
            }
            return userList;
        }

        public async Task<AccountResponseModel> GetAccountById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userModel = new AccountResponseModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName

            };

            return userModel;
        }

        public Task<IEnumerable<MovieCardResponseModel>> GetFavoriate(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FavoriteResponseModel>> GetFavoriteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieCardResponseModel> GetFavoriteMovieDetail(int id, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PurchaseMovieModel>> GetPurchaseById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchaseMovie(int UserId)
        {
            var user = await _userRepository.GetPurchaseMovies (UserId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in user.Purchases)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movieCards;
        }

        public Task<List<MovieReviewsModel>> GetReviews(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserLoginResponseModel> Login(LoginRequestModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser == null)//is reocrd
            {
                return null;
            }

            // get the hashed password and salt from database

            var hashedPassword = GetHashedPassword(model.Password, dbUser.Salt);

            if (hashedPassword == dbUser.HashedPassword)
            {
                // success 
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Email = dbUser.Email
                };

                return userLoginResponseModel;
            }

            return null;
        }

        public Task<ReviewsResponseModel> PostReviews(ReviewsRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseMovieResponseModel> PurchaseMovie(PurchaseMovieModel purchaseMovie)
        {
            throw new NotImplementedException();
        }

        public Task<MovieReviewResponseModel> PutReviews(MovieReviewResponseModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewsResponseModel> PutReviews(ReviewsRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel model)
        {
            // make sure with the user entered email does not exists in database.

            var dbUser = await _userRepository.GetUserByEmail(model.Email);

            if (dbUser != null)
            {
                // user already has email
                throw new ConflictException("Email already exists");
            }

            // user does not exists in the database

            // generate a uniqueue salt

            var salt = GenerateSalt();
            var hashedPassword = GetHashedPassword(model.Password, salt);

            // hash password with salt
            // save the salt and hashed password to the database.

            // create User entity object
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            var createdUser = await _userRepository.AddAsync(user);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userRegisterResponseModel;

        }

        public Task<UnFavoriteResponseModel> removefromFavorite(UnFavoriteRequestModel model)
        {
            throw new NotImplementedException();
        }

        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        Task<List<FavoriteRequestModel>> IUserService.GetFavoriteById(int id)
        {
            throw new NotImplementedException();
        }

        private string GetHashedPassword(string password, string salt)
        {
            // Never ever create your own HAshing Algorithms
            // always use Industry tried and tested HAshing Algorithms
            // Aarogon2 
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                     password: password,
                                                                     salt: Convert.FromBase64String(salt),
                                                                     prf: KeyDerivationPrf.HMACSHA512,
                                                                     iterationCount: 10000,
                                                                     numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}

