using ApplicationCore.Model;
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
    public class UserController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }

        [HttpGet]
        [Route("purchase")]
        public async Task<IActionResult> GetAllPurchases()
        {
            var userId = _currentUserService.UserId;
            //call userservice GetAllPurchases
            var movieCards = await _userService.GetPurchaseMovie(userId);
            return Ok(movieCards);//look like list movie  reuse moviecard
        }

        [HttpGet]
        [Route("favorite")]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = _currentUserService.UserId;
            //call userservice GetAllPurchases
            var movieCards = await _userService.GetFavoriate(userId);
            return Ok(movieCards);
        }
        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchasesMovie([FromBody] PurchaseMovieModel purchaseMovie)
        {
            var movie = await _userService.PurchaseMovie(purchaseMovie);

            if (movie == null)
            {
                return NotFound("Purchase movie filed");
            }
            return Ok(movie);
        }


        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> Favorite([FromBody] FavoriteRequestModel model)
        {
            var favoriteMovieResponse = await _userService.AddToFavorite(model);
            return Ok(favoriteMovieResponse);
        }

        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> UnFavorite([FromBody] UnFavoriteRequestModel model)
        {
            var unfavoriteMovieResponse = await _userService.removefromFavorite(model);
            return Ok(unfavoriteMovieResponse);
        }

        


        //    public async Task<IActionResult> GetProfile()
        //    {
        //        return View();
        //    }


        //    public async Task<IActionResult> EditProfile()
        //    {
        //        return View();
        //    }


        //    public async Task<IActionResult> BuyMovie()
        //    {
        //        return View();
        //    }


        //    public async Task<IActionResult> FavoriteMovie()
        //    {
        //        return View();
        //    }
        //}

    }
}
