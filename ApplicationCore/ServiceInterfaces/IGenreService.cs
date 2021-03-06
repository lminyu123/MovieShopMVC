using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
   public interface IGenreService
    {
        Task<IEnumerable<GenreResponseModel>> GetAllGenres();
        Task<GenreResponseModel> GetGenreDetails(int id);
    }
}
