using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MITT_Intern_2019_10_10.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MITT_Intern_2019_10_10.Controllers
{
    public class CompaniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUserManager _userManager { get; set; }
        public ApplicationSignInManager _signInManager { get; set; }

        // GET: Companies
        public CompaniesController()
        {

        }
        public CompaniesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ApplicationSignInManager SIM
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
        public ApplicationUserManager UM
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

        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,UserName")] Company company)
        {
            if (ModelState.IsValid)
            {
                UM.Create(company, "Password1!");

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,CompanyName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Bio,Skills,SchoolProgram,Teachers,ProfileImage,HeaderImage")] Company company, HttpPostedFileBase profileImage, HttpPostedFileBase headerImage)
        {
            if (ModelState.IsValid)
            {
                //delete old profile image and replace it with the new one
                if (profileImage != null)
                {
                    string filename = Helper.SaveFileFromUser(company.Id, profileImage, Server.MapPath("~"), "profile");
                    company.ProfileImage = filename;

                    //delete old profile images if there are any, this is so you can 
                    //only have one of each image type at a time

                    if (company.ProfileImage != null)
                    {
                        var filesToDelete = Directory.GetFiles(String.Format("{0}\\uploads\\{1}\\profileImage\\", Server.MapPath("~"), company.Id));

                        foreach (var file in filesToDelete)
                        {
                            var fileName = file.Substring(file.LastIndexOf("\\") + 1);

                            if (fileName != company.ProfileImage)
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                    }
                }
                //delete old header image and replace it with the new one
                if (headerImage != null)
                {
                    string filename = Helper.SaveFileFromUser(company.Id, headerImage, Server.MapPath("~"), "header");
                    company.HeaderImage = filename;

                    if (company.HeaderImage != null)
                    {
                        var filesToDelete = Directory.GetFiles(String.Format("{0}\\uploads\\{1}\\HeaderImage\\", Server.MapPath("~"), company.Id));

                        foreach (var file in filesToDelete)
                        {
                            var fileName = file.Substring(file.LastIndexOf("\\") + 1);

                            if (fileName != company.HeaderImage)
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                    }
                }
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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

        public ActionResult CompanyProfile(string id)
        {
            ViewBag.CurrentVisitingId = User.Identity.GetUserId();

            if (id == null)
            {
                var test = DateTime.Now.ToOADate();
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company c = db.Companies.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }

            CompanyViewModel cvm = new CompanyViewModel()
            {
                Id = c.Id,
                Bio = c.Bio,
                Email = c.Email,
                HeaderImage = c.HeaderImage,
                ProfileImage = c.ProfileImage,
                UserName = c.UserName
            };

            return View(cvm);
        }


    }
}
