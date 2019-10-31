using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MITT_Intern_2019_10_10.Models;
namespace MITT_Intern_2019_10_10.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            
            if (db.Companies.Find(userId) != null)
            {
                return RedirectToAction("CompanyProfile", "Companies", new { id = userId});
            }
            else if (db.Students.Find(userId) != null)
            {
                return RedirectToAction("StudentHomePage", "Students", new { });
            }
            else if (userId == null)
            {
                return RedirectToAction("HomePage","Home", new { });
            }



            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult MessagePage(MessageCarrier mc)
        {
            MessageCarrier m = new MessageCarrier { message = mc.message, ctrller = mc.ctrller, actn = mc.actn, UserId = mc.UserId};
            return View(m);
        }
    }
}