using GymRats.Data;
using GymRats.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymRats.Controllers
{
    public class ClassController : Controller
    {
        private ApplicationDbContext context { get; set; }

        public ClassController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var classes = context.Classes.ToList();
            return View(classes);
        }

        public IActionResult Search(string key1)
        {
            if (string.IsNullOrWhiteSpace(key1))
            {
                return RedirectToAction("Index");
            }

            var classes = context.Classes
                .Where(c => c.Name.Contains(key1) || c.Trainer.Contains(key1))
                .ToList();

            return View("Index", classes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            User user = context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add(Class newClass)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            User user = context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index");
            }

            context.Classes.Add(newClass);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}