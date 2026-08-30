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

        // POST/GET: Booking/Book?classId=5
        public IActionResult Book(int classId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Prevent duplicate bookings: same user, same class
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

        // GET: Booking/Index  ("My Bookings")
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Include() pulls the related Class row along with each Booking,
            // instead of just the raw ClassId number
            var myBookings = db.Bookings
                .Include(b => b.Class)
                .Where(b => b.UserId == userId)
                .ToList();

            return View(myBookings);
        }

        // POST/GET: Booking/Cancel?id=12   (Booking's own Id, not ClassId)
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