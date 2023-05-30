using ElevenCourses.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElevenCourses.Controllers
{
    public class TestHomeController : Controller
    {
        private readonly ILogger<TestHomeController> _logger;

        public TestHomeController(ILogger<TestHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
