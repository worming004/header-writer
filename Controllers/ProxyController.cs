using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace header_writer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyController : ControllerBase
    {
        private readonly ILogger<ProxyController> _logger;

        public ProxyController(ILogger<ProxyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var httpClient = new HttpClient();
            var res = await httpClient.GetAsync("http://demo-endpoint:9090/demo-endpoint/text");
            _logger.LogInformation(string.Join(',', res.Headers.Select(h => h.Key)));
            return Ok();
        }
    }
}
