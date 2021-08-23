using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    //attribute routing
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;

            _genreService = genreService;
        }


        //api/movies/toprevenue
        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            if (!movies.Any())//movies have no dataset
            {
                return NotFound("No Movie Found");

            }
            //200 OK
            return Ok(movies); //AUTO TO CREte json
                               //Seroslization=>object ti abother type of object 
                               //c# to json
                               //SeSerization => JSAON TI C#
                               //.NET Core3.0 or less JSON.NET=>3rd party library include
                               //SYSTEM.Text.JSON
                               //along with data you also need to return http status code
        }
        [Route("toprated")]
        [HttpGet]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (!movies.Any())//movies have no dataset
            {
                return NotFound("No Movie Found");

            }
            return Ok(movies);
        }
        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenreId(int id)
        {
            var genres = await _genreService.GetGenreDetails(id);
            var movies = genres.Movies;
            if (!movies.Any())
            {
                return NotFound($"No Movies Found with genreIdl: {id}");
            }
                
            return Ok(movies);
        }
        [Route("{id:int}", Name = "Movie")]
        [HttpGet]
        public async Task<IActionResult> GetMovies(int id)
        {
            var movies = await _movieService.GetMovieDetails(id);

            return Ok(movies);

        }
        [HttpGet]
        [Route("Movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovie();

            return Ok(movies);
        }
       
        [Route("{id}/reviews")]
        [HttpGet]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetMovieReviews(id);
            if (!reviews.Any())
            {
                return NotFound("No Movies Found.");
            }
            return Ok(reviews);
        }
    }
}

