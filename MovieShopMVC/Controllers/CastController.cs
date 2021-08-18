using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.Controllers
{
    public class CastController : Controller
    {
        private readonly ICastService _castService;

        public CastController (ICastService castService)
        {
            _castService = castService;
        }
        public async Task<IActionResult> Details(int id)
        {
            var CastDetail = await _castService.GotCastDetails(id);
            return View(CastDetail);
        }
    }
}


