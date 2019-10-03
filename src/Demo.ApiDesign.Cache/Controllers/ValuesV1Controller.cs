using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ApiDesign.Cache.Controllers
{
    [Route("api/values")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ValuesControllerV1 : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [ResponseCache(VaryByHeader = "api-version", NoStore = true)]
        [Consumes("application/vnd.product.1")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
