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

        public IActionResult Index(string search)
        {
            var query = context.Classes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search) || c.Trainer.Contains(search));
            }

            var classes = query.OrderByDescending(c => c.Time).ToList();

            ViewData["SearchQuery"] = search;
            return View(classes);
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

        [HttpPost]
        public IActionResult Delete(int id)
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

            var classToDelete = context.Classes.FirstOrDefault(c => c.Id == id);
            if (classToDelete != null)
            {
                context.Classes.Remove(classToDelete);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}