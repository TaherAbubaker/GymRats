using GymRats.Data;
using GymRats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return RedirectToAction("Profile");
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
            if (userId == null)
                return RedirectToAction("Login");

            User user = context.Users.FirstOrDefault(u => u.Id == userId);

            var userBookings = context.Bookings
                .Include(b => b.Class)
                .Where(b => b.UserId == userId)
                .OrderBy(b => b.Class.Time)
                .ToList();

            ViewBag.Bookings = userBookings;

            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string oldPassword, string newPassword)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            //i belive we wont need it but why not 
            if (userId == null || user == null)
                return RedirectToAction("Login");

            if (user.PasswordHash == oldPassword)
            {
                user.PasswordHash = newPassword;
                context.SaveChanges();
                return RedirectToAction("Profile");
            }
            else
            {
                ModelState.AddModelError("", "Invalid old password.");
                return View();
            }
        }
    }
}
