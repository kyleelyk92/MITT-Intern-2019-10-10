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
        private ApplicationDbContext db = new ApplicationDbContext();
        ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        #region "ApplicationSignInManager and ApplicationUserManager"
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
                //this is the way to make a new Student;
                //you'll have to update every instance of ApplicationUser when a new controller is made
                //the controller gets generated with ApplicationUser, just switch that with whatever your model class is
                Student s = new Student { UserName = student.Email, Email = student.Email };
                userManager.CreateAsync(s, "Test1234");


                Um.Create(s, password);

                //db.Students.Add(s);
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
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Bio,ProfileImage,HeaderImage,Skills,SchoolProgram,Teachers")] Student student)
        {
            if (ModelState.IsValid)
            {
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
                    Helper.SaveFileFromUser(user.Id, file, Server.MapPath("~"));
                }
                return RedirectToAction("Index", "Students");
            }
            catch
            {
                return RedirectToAction("MessagePage", "Home", new { Error = "Error uploading file, try again" });
            }
        }
    }
}
