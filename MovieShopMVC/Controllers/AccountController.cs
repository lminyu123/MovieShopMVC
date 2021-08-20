using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Model;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            //cookie based authentication
            //form authentication
            if (!ModelState.IsValid)
            {
                return View();
            }


            var user = await _userService.Login(model);
            
            if (user == null) // login can't find
            {
                throw new Exception("Invalid Login");
            }
            //identity some info in the cookies, authentication cookue..claims
            
            //create claim first
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            // identity class..and class
            //go to an bar=>check your identity =>driving lisence
            //go to airport=>check your passport
            //create movie=>claim with role value as admin


            //put claim in indentity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//get your lisence
            //create cookie
            //httpcontext
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


            // Cookies based authentication....
            return LocalRedirect("~/");
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // call the service and repository to hash the password with salt and save to DB

            var regisetredUser = await _userService.RegisterUser(model);
            return RedirectToAction("Login");

        }
    }
}
