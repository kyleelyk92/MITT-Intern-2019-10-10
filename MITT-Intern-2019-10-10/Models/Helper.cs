using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Mvc;
namespace MITT_Intern_2019_10_10.Models
{
    public class Helper
    {   //TODO: Add some helper functions if I need them later;
        //things like, instaniate a new UserManager, stuff like that
        //maybe a random function, those are fun
        public ApplicationUserManager GetUserManager()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            return manager;
        }

        public static void SaveFileFromUser(ApplicationUser user, HttpPostedFileBase file, string filepath)
        {

        }


    }
}