using ApplicationCore.Model;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastResponseModel> GotCastDetails(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var CastDetailModel = new CastResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath=cast.ProfilePath

            };
            CastDetailModel.Movies = new List<MovieDetailsResponseModel>();
            foreach (var movie in cast.MovieCasts)
            {
                CastDetailModel.Movies.Add(new MovieDetailsResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return CastDetailModel;        
        }
    }
}
