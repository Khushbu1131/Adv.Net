using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRP_Management.DTOs;
using TRP_Management.EF;

namespace TRP_Management.Controllers
{
    public class ChannelController : Controller
    {
        TV_ProgramEntities db = new TV_ProgramEntities();
        // GET: Channel
        public ActionResult Index()
        {
            return View();
        }
        public static Channel Convert(ChannelDTO p)
        {
            return new Channel()
            {
                ChannelId = p.ChannelId,
                ChannelName = p.ChannelName,
                EstablishedYear = p.EstablishedYear,
                Country = p.Country
            };
        }

        public static ChannelDTO Convert(Channel p)
        {
            return new ChannelDTO()
            {
                ChannelId = p.ChannelId,
                ChannelName = p.ChannelName,
                EstablishedYear = p.EstablishedYear,
                Country = p.Country

            };

        }

        public static List<ChannelDTO> Convert(List<Channel> data)
        {
            var list = new List<ChannelDTO>();
            foreach (var d in data)
            {
                list.Add(Convert(d));
            }
            return list;
        }

        [HttpGet]

        public ActionResult Create()
        {
            return View(new ChannelDTO());
        }

        [HttpPost]

        public ActionResult Create(ChannelDTO p)
        {
            if (ModelState.IsValid)
            {
                var data = new Channel()
                {
                    ChannelId = p.ChannelId,
                    ChannelName = p.ChannelName,
                    EstablishedYear = p.EstablishedYear,
                    Country = p.Country
                };
                db.Channels.Add(data);
                db.SaveChanges();
                return RedirectToAction("List");

            }
            return View(p);
        }

        public ActionResult List()
        {
            var data = db.Channels.ToList();
            return View(Convert(data));

        }

        public ActionResult Details(int id)
        {
            var data = db.Channels.Find(id);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = db.Channels.Find(id);
            return View(Convert(data));

        }

        [HttpPost]

        public ActionResult Edit(ChannelDTO p)
        {
            var data = db.Channels.Find(p.ChannelId);
            db.Entry(data).CurrentValues.SetValues(p);
            db.SaveChanges();
            return RedirectToAction("List");

        }

        [HttpGet]

        public ActionResult Delete(int id)
        {
            var data = db.Channels.Find(id);
            return View(Convert(data));
        }

        [HttpPost]

        public ActionResult Delete(int ChannelId, string dcsn)
        {
            if (dcsn.Equals("Yes"))
            {
                var data = db.Channels.Find(ChannelId);
                db.Channels.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }



    }
}
    