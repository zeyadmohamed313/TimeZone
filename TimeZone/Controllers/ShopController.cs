using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
