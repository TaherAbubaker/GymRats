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

        [HttpPost]
        public IActionResult Login(String email, String password)
        {
            //learned this on the internet
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index", "Class");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View();
            }
        }
        [HttpGet]
        public IActionResult Logout() 
        { 
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            User user = context.Users.FirstOrDefault(u => u.Id == userId);
            return View(user);
        }
    }
}
