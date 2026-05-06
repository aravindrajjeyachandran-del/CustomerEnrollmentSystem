using Microsoft.AspNetCore.Mvc;

namespace CustomerEnrollment.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction("Enroll", "Customer");
    }
}