using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
   
    public class HomeController : Controller
    {
        private MovieService _movieService;
           
        public HomeController()
        {
            _movieService = new MovieService();
        }

        public IActionResult Index()
        {
            var movies = _movieService.GotTopRevenueMovies()
;            // get top revenue movie and display on the view

            ViewBag.PageTitle = "Top Revenue Movie";
            ViewData["TotalMovies"] = movies.Count();

            return View(movies);
            
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
