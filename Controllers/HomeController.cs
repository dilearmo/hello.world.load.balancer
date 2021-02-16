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
            HttpContext.Response.Headers.Add("ServerName", _configSettings.Value.ServerName);
            ViewBag.ServerName = _configSettings.Value.ServerName;
            return View();
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
