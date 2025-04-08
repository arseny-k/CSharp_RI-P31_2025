using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }
    }
}
