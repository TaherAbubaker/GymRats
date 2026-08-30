using GymRats.Data;
using GymRats.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymRats.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext context { get; set; }

        public AccountController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
