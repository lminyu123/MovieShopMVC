using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopMVC.Controllers
{
    // GET: /<controller>/

    [Authorize]
    public class UserController : Controller
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> GetAllPurchases()
        {
            var userId = _currentUserService.UserId;
            //call userservice GetAllPurchases
            var movieCards = await _userService.GetPurchaseMovie(userId);
            return Ok(movieCards);//look like list movie  reuse moviecard
        }

        [Authorize]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = _currentUserService.UserId;
            //call userservice GetAllPurchases
            var movieCards = await _userService.GetFavoriate(userId);
            return Ok(movieCards);
        }

        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> FavoriteMovie()
        {
            return View();
        }
    }
}

