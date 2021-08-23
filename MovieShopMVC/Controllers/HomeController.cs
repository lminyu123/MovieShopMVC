using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;



namespace MovieShopMVC.Controllers
{
   
    public class HomeController : Controller
    {
        private IMovieService _movieService;
           
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task <IActionResult> Index()
        {
            //throw new Exception(message: "some exception happend");
            var movieCards =await _movieService.GetTopRevenueMovies();
            // get top revenue movie and display on the view

            return View(movieCards);
            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
