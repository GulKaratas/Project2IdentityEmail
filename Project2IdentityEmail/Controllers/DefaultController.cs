using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
