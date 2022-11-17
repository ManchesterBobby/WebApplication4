using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ParticipantsController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var participant = new ScheduledMeetingViewModel();
            try
            {
                GetMeetings();
                GetEmployees();
            }
            catch
            {
                throw;
            }

            return View(participant);
        }

        [HttpPost]
        public IActionResult Index(ScheduledMeetingViewModel newParticipant)
        {
            try
            {
                GetMeetings();
                GetEmployees();

                var context = new MeetingsContext();

                // Check if the participant is already on another meeting
                var meeting = context.Meetings.FirstOrDefault(x => x.MeetingId == newParticipant.MeetingId);
                var booked = from t1 in context.ScheduledMeetings
                             join t2 in context.Meetings 
                             on t1.MeetingId equals t2.MeetingId
                             where t1.MeetingId == t2.MeetingId
                             select t2;

                if (newParticipant.MeetingId == 0 || meeting == null)
                {
                    ModelState.AddModelError("MeetingId", "The meeting is invalid");
                }
                 else
                {
                    if (booked != null && booked.Any(x => x.MeetingTime == meeting.MeetingTime))
                    {
                        ModelState.AddModelError("EmployeeId", "The participant is already attending another meeting");

                    }
                }

                if (ModelState.IsValid)
                {
                    var dbParticipant = new ScheduledMeeting()
                    {
                        MeetingId = newParticipant.MeetingId,
                        EmployeeId = newParticipant.EmployeeId
                    };

                    context.Add(dbParticipant);
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

        private void GetMeetings()
        {
            using (var context = new MeetingsContext())
            {
                var meetings = context.Meetings.Where(x => x.MeetingTime > DateTime.Now).ToList();
                ViewBag.Meetings = meetings;
            }
        }

        private void GetEmployees()
        {
            using (var context = new MeetingsContext())
            {
                var employees = context.Employees.OrderBy(x => x.Description).ToList();
                ViewBag.Employees = employees;
            }
        }

    }
}
