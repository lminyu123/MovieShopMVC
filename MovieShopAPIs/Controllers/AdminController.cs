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
    public class AdminController : ControllerBase
    {
        
            // GET: api/<AdminController>
            //[HttpGet]
            //public IEnumerable<string> Get()
            //{
            //    return new string[] { "value1", "value2" };
            //}

            // GET api/<AdminController>/5
            [HttpGet("purchase")]
            public string Get(int id)
            {
                return "value";
            }

            // POST api/<AdminController>
            [HttpPost]
            public void Post([FromBody] string value)
            {
            }

            // PUT api/<AdminController>/5
            [HttpPut]
            [Route("movie")]
            public void Put(int id, [FromBody] string value)
            {
            }

            //// DELETE api/<AdminController>/5
            //[HttpDelete("{id}")]
            //public void Delete(int id)
            //{
            //}
        
    }
}
