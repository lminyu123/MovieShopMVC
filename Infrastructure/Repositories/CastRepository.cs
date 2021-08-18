using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
   public class CastRepository: EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Cast> GetByIdAsync(int id)
        {
            var Cast = await _dbContext.Casts.Include(c => c.MovieCasts).ThenInclude(c => c.Movie).FirstOrDefaultAsync(c=>c.Id==id);
              return Cast;
        }

        //public override async Task<Cast> (int id)
        //{
        //    var Cast = await _dbContext.Casts.Include(c => c.Id == Id).ThenInclude()
        //    return Cast;
        //}


    }
}
