using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                //method login do controller account
                return RedirectToAction("Login","Account");
            }
        }
    }
}
