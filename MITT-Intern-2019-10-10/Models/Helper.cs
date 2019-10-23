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
        public static ApplicationUserManager GetUserManager()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            return manager;
        }



        public static void SaveFileFromUser(string userId, HttpPostedFileBase file, string basepath, string profileImageOrHeader)
        {
            string saveId = userId;
            string filetype = "";

            //filepath goes like this
            //~\\uploads\\ID\\images OR ~\\uploads\\ID\\resume

            string fileExtension = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);

            if(fileExtension == "jpg" || fileExtension == "jpeg" || fileExtension == "png")
            {
                if(profileImageOrHeader == "header")
                {
                    filetype = "headerImage";
                }else if(profileImageOrHeader == "profile")
                {
                    filetype = "profileImage";
                }
            }
            if(fileExtension == "pdf")
            {
                filetype = "resume";
            }

            string pathend = String.Format("uploads\\{0}\\{1}\\{2}", saveId, filetype, file.FileName);
            var fullpath = Path.Combine(basepath, pathend);

            string fullDirectoryName = basepath + String.Format("uploads\\{0}\\{1}", saveId, filetype);

            if (Directory.Exists(fullDirectoryName))
            {
                file.SaveAs(fullpath);
            }
            else
            {
                Directory.CreateDirectory(fullDirectoryName);
                file.SaveAs(fullpath);
            }
        }


    }
}