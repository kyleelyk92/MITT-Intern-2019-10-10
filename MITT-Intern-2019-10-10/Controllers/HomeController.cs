using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MITT_Intern_2019_10_10.Models;
namespace MITT_Intern_2019_10_10.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //Student student = new Student();
            //Helper.SaveFileFromUser(student);

            
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
        public ActionResult MessagePage(string Message, object messageValues)
        {
            ViewBag.Message = Message;
            return View();
        }
    }
}