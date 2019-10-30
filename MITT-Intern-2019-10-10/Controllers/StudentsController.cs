using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MITT_Intern_2019_10_10.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
            ViewBag.CurrentUserId = User.Identity.GetUserId();

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
        public ActionResult Create([Bind(Include = "Email,UserName")] Student student, string password)
        {
            if (ModelState.IsValid)
            {
                Student s = new Student { UserName = student.Email, Email = student.Email };
                var result = Um.Create(s, password);
                if (result.Succeeded)
                {
                    MessageCarrier messCarr = new MessageCarrier() { actn = "Index", ctrller = "Students", message = "Successfully enrolled student" };
                    db.SaveChanges();

                    return RedirectToAction("MessagePage", "Home", messCarr);
                }
                else
                {
                    return View(student);
                }
            }
            return View(student);
        }


        #region student edit functionality
        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Where(s => s.Id == id).Include(stude => stude.Skills).Include(st => st.SchoolProgram).FirstOrDefault();

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
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Bio,Skills,SchoolProgram,Teachers,ProfileImage,HeaderImage")] Student student, HttpPostedFileBase profileImage, HttpPostedFileBase headerImage)
        {
            if (ModelState.IsValid)
            {
                //delete old profile image and replace it with the new one
                if (profileImage != null)
                {
                    string filename = Helper.SaveFileFromUser(student.Id, profileImage, Server.MapPath("~"), "profile");
                    student.ProfileImage = filename;

                    //delete old profile images if there are any, this is so you can 
                    //only have one of each image type at a time

                    if (student.ProfileImage != null)
                    {
                        var filesToDelete = Directory.GetFiles(String.Format("{0}\\uploads\\{1}\\profileImage\\", Server.MapPath("~"), student.Id));

                        foreach (var file in filesToDelete)
                        {
                            var fileName = file.Substring(file.LastIndexOf("\\") + 1);

                            if (fileName != student.ProfileImage)
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                    }
                }
                //delete old header image and replace it with the new one
                if (headerImage != null)
                {
                    string filename = Helper.SaveFileFromUser(student.Id, headerImage, Server.MapPath("~"), "header");
                    student.HeaderImage = filename;

                    if (student.HeaderImage != null)
                    {
                        var filesToDelete = Directory.GetFiles(String.Format("{0}\\uploads\\{1}\\HeaderImage\\", Server.MapPath("~"), student.Id));

                        foreach (var file in filesToDelete)
                        {
                            var fileName = file.Substring(file.LastIndexOf("\\") + 1);

                            if (fileName != student.HeaderImage)
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                    }
                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        #endregion



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
            Student s = db.Students.Include(x=> x.SchoolProgram).Include(st => st.Skills).FirstOrDefault(stud => stud.Id == id);
            if (s == null)
            {
                return HttpNotFound();
            }

            StudentViewModel svm = new StudentViewModel()
            {
                Id = s.Id,
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
            return View(s);
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



        //TODO: figure out how you want to add skills to somebody
        public ActionResult AddSkills(string studentId)
        {
            var s = db.Students.Where(st => st.Id == studentId).Include(stu => stu.Skills).ToList();

            var student = s[0];
            var studentSkills = student.Skills.ToList();
            var allSkills = db.Skills.ToList();
            ViewBag.Skills = new List<Skill>();

            foreach(var sk in allSkills)
            {
                if (!studentSkills.Contains(sk))
                {
                    ViewBag.Skills.Add(sk);
                }
            }
            

            return View(student);
        }
        [HttpPost]
        public ActionResult AddSkills(int[] skillsToAdd, string studentId)
        {
            //skillsToAdd comes in as an array of IDs of the skills
            var studId = studentId;
            var s = db.Students.Where(st => st.Id == studentId).Include(stu => stu.Skills).ToList();
            var student = s[0];

            foreach (var skill in skillsToAdd)
            {
                var sk = db.Skills.Find(skill);

                if (!student.Skills.Contains(sk))
                {
                    student.Skills.Add(sk);
                }
            }

            db.SaveChanges();
            var whatIsThis = skillsToAdd;
            return RedirectToAction("Index", "Students");
        }
        public ActionResult Removeskills(string studentId)
        {
            var s = db.Students.Where(st => st.Id == studentId).Include(stu => stu.Skills).ToList();

            var student = s[0];
            var studentSkills = student.Skills.ToList();
            var allSkills = db.Skills.ToList();
            ViewBag.Skills = new List<Skill>();

            foreach (var sk in allSkills)
            {
                if (studentSkills.Contains(sk))
                {
                    ViewBag.Skills.Add(sk);
                }
            }


            return View(student);
        }
        [HttpPost]
        public ActionResult Removeskills(int[] skillsToRemove, string studentId)
        {
            //skillsToAdd comes in as an array of IDs of the skills
            var studId = studentId;
            var s = db.Students.Where(st => st.Id == studentId).Include(stu => stu.Skills).ToList();
            var student = s[0];

            foreach (var skill in skillsToRemove)
            {
                var sk = db.Skills.Find(skill);

                if (student.Skills.Contains(sk))
                {
                    student.Skills.Remove(sk);
                }
            }

            db.SaveChanges();
                
            return RedirectToAction("Index", "Students");
        }
        public ActionResult ChangeProgram(string studentId)
        {
            var student = db.Students.FirstOrDefault(st => st.Id == studentId);
            var programs = db.Programs.ToList();

            var counter = programs.Count-1;
            //takes the one that they're registered in out of the list
            
            for(var x = 0; x < counter; x++)
            {
                if (student.SchoolProgram != null)
                {
                    if (student.SchoolProgram.Id == programs[x].Id)
                    {
                        programs.Remove(programs[x]);
                    }
                }
            }
            ViewBag.Programs = programs;

            return View(student);
        }
        [HttpPost]
        public ActionResult ChangeProgram(string studentId, int programId)
        {
            var program = db.Programs.Find(programId);
            var student = db.Students.Find(studentId);

            student.SchoolProgram = program;
            db.SaveChanges();
            return RedirectToAction("MessagePage","Home", new { studentId = studentId, programId = programId, programName = program.Title, Message = "Updated student program"});
        }

        public ActionResult AddResume(string studentId)
        {
            Student s = db.Students.Find(studentId);
            return View(s);
        }

        [HttpPost]
        public ActionResult AddResume(string studentId, HttpPostedFileBase resume)
        {
            string filepath = Helper.SaveFileFromUser(studentId, resume, Server.MapPath("~"), "");
            var student = db.Students.Find(studentId);
            student.HasResume = true;
            student.ResumeLink = filepath;

            //delete old resume
            if (student.ResumeLink != null)
            {
                var filesToDelete = Directory.GetFiles(String.Format("{0}\\uploads\\{1}\\resume\\", Server.MapPath("~"), student.Id));

                foreach (var file in filesToDelete)
                {
                    var fileName = file.Substring(file.LastIndexOf("\\") + 1);

                    if (fileName != student.ResumeLink)
                    {
                        System.IO.File.Delete(file);
                    }
                }
            }

            db.SaveChanges();
            
            return RedirectToAction("MessagePage", "Home", new MessageCarrier() {ctrller = "Students", actn="Edit", UserId = studentId, message="Succesfully uploaded resume" });
        }

        public ActionResult StudentHomePage()
        {
            var userId = User.Identity.GetUserId();
            Student user = db.Students.Include(stu => stu.SchoolProgram).FirstOrDefault(st => st.Id == userId);

            if (user != null)
            {
                //myId = e2ce1084-4830-460d-a819-e28e8a30bc16
                
                if (user.SchoolProgram != null)
                {
                    var sp = user.SchoolProgram;
                    var postings = db.Postings.Where(posting => posting.SchoolProgram.Id == sp.Id).ToList();
                    return View(postings);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            
            
        }
    }
    
    #endregion
}
