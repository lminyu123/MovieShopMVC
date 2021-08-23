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
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }
        [HttpGet]
        [Route("{int:id}", Name ="CastDetail")]
        public async Task<IActionResult> Details([FromBody] int id)
        {
            var CastDetail = await _castService.GotCastDetails(id);
            if (CastDetail == null)
            {
                return NotFound("No Cast Found");
            }
            return Ok(CastDetail);
        }
    }
}
