using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNetCore.Identity;
using MITT_Intern_2019_10_10.Models;
using System.IO;


namespace MITT_Intern_2019_10_10.Controllers
{
    public class StudentsController : Controller
    {
        #region Constructor and assignment of managers Region
        private ApplicationDbContext db = new ApplicationDbContext();
        ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        
        //I took the code for the new signinmanager and usermanager from the account controller
        //if we want, we can just do all of our signins from that controller
        //otherwise, I have them imported to this controller in the sections about signinmanager/usermanager
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        
        //in order to get the sign in and user managers, create 2 constructors, one empty and one like the one below
        public StudentsController()
        {

        }
        //this constructor initializes the usermanager and signin manager elements
        public StudentsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            Um = userManager;
            Sim = signInManager;
        }

        //this is the get and set methods for the sign in manager
        // these are accessed when you want to use the sign in manager
        public ApplicationSignInManager Sim
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        //this is the user manager, call it when you want to access the user manager functions 
        //like 
        //um.Create(new User)
        public ApplicationUserManager Um
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region controllers for basic functionality
        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5

        public ActionResult DetailsShort(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,FirstName,LastName,UserName")] Student student, string password)
        {
            if (ModelState.IsValid)
            {
                Student s = new Student { UserName = student.Email, Email = student.Email };
                userManager.CreateAsync(s, "Test1234");

                Um.Create(s, password);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            //5b8f32aa-6c35-4504-9cf3-82a64c3c800e is a student ID i can use for testing
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Bio,Skills,SchoolProgram,Teachers")] Student student, HttpPostedFileBase profileImage, HttpPostedFileBase headerImage)
        {
            if (ModelState.IsValid)
            {
                //delete old profile image and replace it with the new one
                if (profileImage != null)
                {
                    Helper.SaveFileFromUser(student.Id, profileImage, Server.MapPath("~"), "profile");

                    var allFiles = Directory.GetFiles(Server.MapPath("~") + String.Format("uploads\\{0}\\profileImage\\", student.Id));
                    //this function deletes all the files in the folder and replaces it with the right one
                    //everybody can only have one file of each type
                    foreach (var file in allFiles)
                    {
                        if (System.IO.File.Exists(file) && !file.Contains(profileImage.FileName))
                        {
                            System.IO.File.Delete(file);
                        }
                    }

                    //handling the student profile string change in here as well, not in the helper
                    //so i'll have to do this every time i do a file upload in a controller
                    student.ProfileImage = profileImage.FileName;
                }
                //delete old header image and replace it with the new one
                if(headerImage != null)
                {
                    //run the helper to save a file first, in case it breaks
                    Helper.SaveFileFromUser(student.Id, headerImage, Server.MapPath("~"), "header");

                    var allFiles = Directory.GetFiles(Server.MapPath("~") + String.Format("uploads\\{0}\\headerImage\\", student.Id));
                    
                    foreach (var file in allFiles)
                    {
                     //this loop deletes all the files in the folder and replaces it with the right one
                     //everybody can only have one file of each type
                        if (System.IO.File.Exists(file) && !file.Contains(headerImage.FileName))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    student.HeaderImage = headerImage.FileName;
                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Users.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult StudentProfile(string id)
        {
            ViewBag.CurrentVisitingId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student s = db.Students.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            
            StudentViewModel svm = new StudentViewModel()
            {
                Bio = s.Bio,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                SchoolProgram = s.SchoolProgram,
                HeaderImage = s.HeaderImage,
                ProfileImage = s.ProfileImage,
                Skills = s.Skills,
                UserName = s.UserName
            };
            return View(svm);
        }

        public ActionResult PracticeFileUpload()
        {
            var checkthis = Server.MapPath("~");
            return View();
        }

        [HttpPost]
        public ActionResult PracticeFileUpload(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    var user = db.Users.Find(User.Identity.GetUserId());
                    //this takes a userId, the httppostedbasefile, and the base path of whatever controller you're in
                    //Helper.SaveFileFromUser(user.Id, file, Server.MapPath("~"));
                }
                return RedirectToAction("Index", "Students");
            }
            catch
            {
                return RedirectToAction("MessagePage", "Home", new { Error = "Error uploading file, try again" });
            }
        }
    }
    #endregion
}
