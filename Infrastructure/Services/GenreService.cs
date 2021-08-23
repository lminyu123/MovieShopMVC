using ApplicationCore.Entities;
using ApplicationCore.Model;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreReposistory;
        private readonly IMemoryCache _memoryCache;

        public GenreService(IAsyncRepository<Genre> genreReposistory, IMemoryCache memoryCache)
        {
            _genreReposistory = genreReposistory;
            _memoryCache = memoryCache;

        }
        public async Task<IEnumerable<GenreResponseModel>> GetAllGenres()
        {
            var genres = await _memoryCache.GetOrCreateAsync("genresData", async entry =>
             {
                 entry.SlidingExpiration = TimeSpan.FromDays(30);
                 return await _genreReposistory.ListAllAsync();//if "genresdata" don't have any data, will go to return db,insert data to genresdata
             });
            //var genres = await _genreReposistory.ListAllAsync();

            var genresModel = new List<GenreResponseModel>();

            foreach (var genre in genres)
            {
                genresModel.Add(new GenreResponseModel { Id = genre.Id, Name = genre.Name });
            }
            return genresModel;

        }
        public async Task<GenreResponseModel> GetGenreDetails(int id)
        {
            var genre = await _genreReposistory.GetByIdAsync(id);
            var genreDetails = new GenreResponseModel()
            {
                Id = genre.Id,
                Name = genre.Name
            };

            genreDetails.Movies = new List<MovieCardResponseModel>();
            foreach (var movie in genre.Movies)
            {
                genreDetails.Movies.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }

            return genreDetails;


        }


    }
}
