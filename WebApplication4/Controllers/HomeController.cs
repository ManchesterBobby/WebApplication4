using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var meeting = new MeetingViewModel() { MeetingTime = DateTime.Now};
            try
            {
                GetRooms();
            }
            catch 
            {
                throw;
            }

            return View(meeting);
        }

        [HttpPost]
        public IActionResult Index(MeetingViewModel newMeeting)
        {
            try
            {
                GetRooms();

                var context = new MeetingsContext();

                // Check if the room is already booked
                var booked = context.Meetings.Where(x => x.RoomId == newMeeting.RoomId && x.MeetingTime == newMeeting.MeetingTime).ToList();

                if (booked != null && booked.Count > 0) {
                    ModelState.AddModelError("MeetingTime", "The meeting room is already booked at the selected time");
                }

                if (ModelState.IsValid)
                {
                    var dbMeeting = new Meeting()
                    {
                        MeetingTime = newMeeting.MeetingTime,
                        RoomId = newMeeting.RoomId,
                        Description = newMeeting.Description
                    };

                    context.Add(dbMeeting);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                throw;
            }

            return View();

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void GetRooms()
        {
            using (var context = new MeetingsContext())
            {
                var rooms = context.Rooms.ToList();
                ViewBag.Rooms = rooms;
            }
        }
    }
}