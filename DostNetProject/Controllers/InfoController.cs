using Microsoft.AspNetCore.Mvc;

namespace DostNetProject.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact() => View();
        public IActionResult PrivacyPolicy() => View();
        public IActionResult Terms() => View();
        public IActionResult Report() => View();
        public IActionResult Jobs() => View();
        public IActionResult News() => View();
    }
}
