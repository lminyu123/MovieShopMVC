using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        
        public async Task<IActionResult> Index([FromBody] int id)
        {
            // Show List of Genres in the Header of LayoutPage Hint: Use Partial View and use Bootstarp DropDown to show genres 
            // Use <a> with name og genre and that when clicked go to database and show list of movies belong to that genre
           
            var genres = await _genreService.GetAllGenres();
            if(genres == null)
            {
                return NotFound("No Gneres Found");
            }
            return Ok(genres);
        }
    }
}