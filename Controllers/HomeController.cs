using hello.world.load.balancer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace hello.world.load.balancer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<ConfigSettings> _configSettings;

        public HomeController(
            ILogger<HomeController> logger,
            IOptions<ConfigSettings> configSettings)
        {
            _logger = logger;
            _configSettings = configSettings;
        }

        public IActionResult Index()
        {
            ViewBag.server_who_loaded_page = _configSettings.Value.ServerName;
            ViewBag.client = System.Environment.MachineName;
            return View();
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromForm]Data data) {
            var server_who_saved_submission = _configSettings.Value.ServerName;
            SqlOperations.Insert(data.server_who_loaded_page, server_who_saved_submission, data.color, data.client, _configSettings.Value.ConnectionString);

            var result = SqlOperations.Read(data.client, _configSettings.Value.ConnectionString);

            return Ok(result);
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
