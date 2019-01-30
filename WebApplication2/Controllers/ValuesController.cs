using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private WebProxyServerProvider webProxyServer;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public ValuesController(WebProxyServerProvider proxy)
        {
            webProxyServer = proxy;

        }

        // Handle all requests :)
        [HttpPost]
        public async Task Post()
        {
            await webProxyServer.HandleRequestAsync(HttpContext);
        }
    }
}
