using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Model;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService //if get error need go back to Imvs change the a task
    {
        //需要去start up里inject一下
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)// call the method in moviecontroller
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var MovieDetailsModel = new MovieDetailsResponseModel
            {

                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price
            };
            MovieDetailsModel.Casts = new List<CastResponseModel>();
            foreach (var cast in movie. MovieCasts)
            {
                MovieDetailsModel.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Character = cast.Character,
                    Gender=cast.Cast.Gender,
                    ProfilePath=cast.Cast.ProfilePath
                });
            }
            MovieDetailsModel.Genres = new List<GenreResponseModel>();
            foreach (var genre in movie.Genres)
            {
                MovieDetailsModel.Genres.Add(new GenreResponseModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });

            }
            return MovieDetailsModel;
        }
        
        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.Get30HighestRevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();

            //need convert movie to list
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {

                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl


                });
            }

            return movieCards;
        }

        public async  Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
        
            var movies = await _movieRepository.Get30HighestRevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();

            //need convert movie to list
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel {

                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl


                });
            }

            return movieCards;

        }
        public async Task<List<MovieDetailsResponseModel>> GetAllMovie()
        {
            var movies = await _movieRepository.ListAllAsync();
            var AllMovies = new List<MovieDetailsResponseModel>();
            foreach (var movie in movies)
            {
                AllMovies.Add(new MovieDetailsResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }

            return AllMovies;
        }



        public async Task<IEnumerable<MovieReviewResponseModel>> GetMovieReviews(int id)
        {
            var movies = await _movieRepository.GetReviews(id);
            var movieReview = new List<MovieReviewResponseModel>();
            foreach (var movie in movies)
            {
                movieReview.Add(new MovieReviewResponseModel
                {
                    MovieId = movie.MovieId,
                    UserId = movie.UserId,
                    Title = movie.Movie.Title,
                    FirstName = movie.User.FirstName,
                    LastName = movie.User.LastName,
                    Rating = movie.Rating,
                    PosterUrl = movie.Movie.PosterUrl,
                    ReviewText = movie.ReviewText
                });
            }
            return movieReview;
        }
    }
    
}