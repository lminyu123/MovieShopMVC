using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.Model;

namespace MovieShopMVC.Controllers
{
    public class CastController : Controller
    {
        private readonly ICastService _castService;
        private readonly ICastRepository _castRepository;
        public CastController (ICastService castService, ICastRepository castRepository)
        {
            _castService = castService;
            _castRepository = castRepository;
        }
        public async Task<CastResponseModel> Details(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castModel = new CastResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath = cast.ProfilePath
            };

            castModel.Movies = new List<MovieDetailsResponseModel>();

            foreach (var movie in cast.MovieCasts)
            {
                castModel.Movies.Add(new MovieDetailsResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }

            return castModel;
        }
    }
}


