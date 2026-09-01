using GymRats.Data;
using GymRats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext db;

        public BookingController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Book(int classId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            bool alreadyBooked = db.Bookings
                .Any(b => b.UserId == userId && b.ClassId == classId);

            if (!alreadyBooked)
            {
                var booking = new Booking
                {
                    UserId = userId.Value,
                    ClassId = classId
                };

                db.Bookings.Add(booking);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Class");
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var myBookings = db.Bookings
                .Include(b => b.Class)
                .Where(b => b.UserId == userId)
                .ToList();

            return View(myBookings);
        }

        public IActionResult Cancel(int id)
        {
            var booking = db.Bookings.Find(id);

            if (booking != null)
            {
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}