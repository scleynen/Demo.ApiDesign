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
    [ApiVersion("2.0")]
    [ResponseCache(VaryByHeader = "api-version", NoStore = true)]
    public class ValuesControllerV2 : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Consumes("application/vnd.product.2")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1-v2", "value2-v2" };
        }
    }
}
