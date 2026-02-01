using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminSessionKey = "IsAdmin";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Username == "admin" && model.Password == "admin123")
            {
                HttpContext.Session.SetString(AdminSessionKey, "true");
                return RedirectToAction("Index", "Automobili");
            }

            ViewBag.Error = "Pogrešno korisničko ime ili lozinka.";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }

        public static bool IsAdmin(HttpContext context)
        {
            return context.Session.GetString(AdminSessionKey) == "true";
        }
    }
}