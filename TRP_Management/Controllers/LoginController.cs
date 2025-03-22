using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRP_Management.DTOs;
using TRP_Management.EF;

namespace TRP_Management.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        TV_ProgramEntities db = new TV_ProgramEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginDTO log)
        {
            //
            var user = (from u in db.Users
                        where u.UName.Equals(log.UName) &&
                        u.Password.Equals(log.Password)
                        select u).SingleOrDefault();
            if (user != null)
            {
                Session["user"] = user; //boxing
                return RedirectToAction("List", "Program");
            }
            TempData["Msg"] = "User not found";
            return View();
        }
    }
}