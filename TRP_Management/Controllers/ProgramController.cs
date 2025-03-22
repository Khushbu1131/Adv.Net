using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRP_Management.DTOs;
using TRP_Management.EF;
using TRP_Management.Auth;
namespace TRP_Management.Controllers
{
    [Logged] 
    public class ProgramController : Controller
    {
        // GET: Program
        TV_ProgramEntities db = new TV_ProgramEntities();
        // GET: Channel
        public ActionResult Index()
        {
            return View();
        }
        public static Program Convert(ProgramDTO p)
        {
            return new Program()
            {
                ProgramId = p.ProgramId,
                ProgramName = p.ProgramName,
                TRPScore = p.TRPScore,
                ChannelId = p.ChannelId,
                AirTime=p.AirTime
            };
        }

        public static ProgramDTO Convert(Program p)
        {
            return new ProgramDTO()
            {
                ProgramId = p.ProgramId,
                ProgramName = p.ProgramName,
                TRPScore = p.TRPScore,
                ChannelId = p.ChannelId,
                AirTime = p.AirTime
            };

        }

        public static List<ProgramDTO> Convert(List<Program> data)
        {
            var list = new List<ProgramDTO>();
            foreach (var d in data)
            {
                list.Add(Convert(d));
            }
            return list;
        }

        [HttpGet]

        public ActionResult Create()
        {
            ViewBag.UName = Session["user"]?.ToString();
            return View(new ProgramDTO());
        }

        [HttpPost]
       // [Authorize(Roles = "admin")]
        public ActionResult Create(ProgramDTO p)
        {
            if (ModelState.IsValid)
            {
                string userName = Session["user"]?.ToString();
                var data = new Program()
                {
                    ProgramId = p.ProgramId,
                    ProgramName = p.ProgramName,
                    TRPScore = userName == "admin" ? p.TRPScore : 0, // Only allow admin to set TRPScore
                    // TRPScore=p.TRPScore,
                    ChannelId = p.ChannelId,
                    AirTime = p.AirTime
                };
                db.Programs.Add(data);
                db.SaveChanges();
                return RedirectToAction("List");

            }
            return View(p);
        }

        public ActionResult List(string searchTerm, string filterChannel)
        {
            // Start with all programs
            var programs = db.Programs.AsQueryable();

            // Apply search filter (by name or TRP score)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                programs = programs.Where(p => p.ProgramName.Contains(searchTerm) ||
                                               p.TRPScore.ToString().Contains(searchTerm));
            }

            // Apply channel filter
            if (!string.IsNullOrEmpty(filterChannel))
            {
                if (int.TryParse(filterChannel, out int channelId))
                {
                    programs = programs.Where(p => p.ChannelId == channelId);
                }
            }

            // Convert data to DTO
            var programDTOs = Convert(programs.ToList());

            // Pass channels to ViewBag for dropdown
            ViewBag.Channels = db.Channels.Select(c => new SelectListItem
            {
                Value = c.ChannelId.ToString(),
                Text = c.ChannelName
            }).ToList();

            return View(programDTOs);
        }
       // [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var data = db.Programs.Find(id);
            return View(data);
        }

        [HttpGet]
       
        public ActionResult Edit(int id)
        {
            var data = db.Programs.Find(id);
            return View(Convert(data));

        }

        [HttpPost]

        public ActionResult Edit(ProgramDTO p)
        {
            var data = db.Programs.Find(p.ProgramId);
           
                db.Entry(data).CurrentValues.SetValues(p);
            
                db.SaveChanges();
            return RedirectToAction("List");

        }

        [HttpGet]

        public ActionResult Delete(int id)
        {
            var data = db.Programs.Find(id);
            return View(Convert(data));
        }

        [HttpPost]

        public ActionResult Delete(int ProgramId, string dcsn)
        {
            if (dcsn.Equals("Yes"))
            {
                var data = db.Programs.Find(ProgramId);
                db.Programs.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }



    }
}
    
    