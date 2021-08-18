using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class MovieRespositoy : EfRepository<Movie>, IMovieRepository
    {
        public MovieRespositoy(MovieShopDbContext dbContext): base(dbContext)
        {

        }
     
        public async Task<IEnumerable<Movie>> Get30HighestRevenueMovies()
        {
            // get 30 movies from table order by revenue
            //select top 30 from movies order by revenue;
            //come from base class
            //tolist(),count(),or loop through
            //I/O BOUND OPERATION
            //EF has method that have both async and non-async ones
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;

        }

        public override async Task<Movie> GetByIdAsync(int Id)
        {
            // movie table, then genres, then casts and rating
            // include() theninclude()
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres)
                 .FirstOrDefaultAsync(m => m.Id == Id);// use include prop join the navigation prop
            
                
                if (movie == null)
            {
                throw new Exception($"No Movie Found for the id {Id}");
            }

            var movieRating = await _dbContext.Reviews.Where(m => m.MovieId == Id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating); // using review for get avg rating  to retuern whole object  

            movie.Rating = movieRating;
            return movie;
        }


    }
}
