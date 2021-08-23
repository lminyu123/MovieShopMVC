using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>,IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

            public override async Task<Genre> GetByIdAsync(int id)
        {
            var genre = await _dbContext.Genres.Include(g => g.Movies).FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                throw new Exception($"No genre found with {id}");
            }
            return genre;
        }
        
    }
}
