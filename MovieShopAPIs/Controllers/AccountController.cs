using ApplicationCore.Model;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        public AccountController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            var user = await _userService.RegisterUser(model);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _userService.Login(model);
            return Ok(user);

        }
        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            var users = await _userService.GetAccount();
            if (!users.Any())
            {
                NotFound("No Accounts exist");
            }
            return Ok(users);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var user = await _userService.GetAccountById(id);
            if (user == null)
            {
                NotFound($"Account dosen't exist with id {id}");
            }
            return Ok(user);

        }


        //[HttpGet]
        //[Route("{int:id}", Name = "GetUser")]
        //public async Task <IActionResult>




    }
}
