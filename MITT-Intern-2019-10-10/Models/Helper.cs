﻿using Microsoft.AspNet.Identity.EntityFramework;
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


        public static string SaveFileFromUser(string userId, HttpPostedFileBase file, string basepath, string profileImageOrHeader)
        {
            string saveId = userId;
            string filetype = "";

            //filepath goes like this
            //~\\uploads\\ID\\images OR ~\\uploads\\ID\\resume

            string fileExtension = Path.GetExtension(file.FileName);

            if(fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
            {
                if(profileImageOrHeader == "header")
                {
                    filetype = "headerImage";
                }else if(profileImageOrHeader == "profile")
                {
                    filetype = "profileImage";
                }
            }
            if(fileExtension == ".pdf")
            {
                filetype = "resume";
            }

            int unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            string newName = String.Format("{0}{1}", unixTimestamp, saveId);
            string pathend = String.Format("uploads\\{0}\\{1}\\{2}{3}", saveId, filetype, newName, fileExtension);
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
            return String.Format("{0}{1}",newName, fileExtension);
        }
    }
}