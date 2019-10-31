using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MITT_Intern_2019_10_10.Models;
using System.Net.Mail;
using System.IO;


namespace MITT_Intern_2019_10_10.Controllers
{
    public class PostingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Postings
        public ActionResult Index()
        {
            return View(db.Postings.ToList());
        }

        // GET: Postings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posting posting = db.Postings.Include(x => x.Skills).Include(x => x.Company).FirstOrDefault(x => x.Id == id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }

        // GET: Postings/Create
        public ActionResult Create()
        {
            ViewBag.Skills = db.Skills.ToList();
            return View();
        }

        // POST: Postings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,ClosingDate")] Posting posting, int[] skillsToAdd)
        {

            var userId = User.Identity.GetUserId();
            Company company = db.Companies.Find(userId);

            posting.PostingDate = DateTime.Now;

            foreach (int skillId in skillsToAdd)
            {
                Skill skill = db.Skills.Find(skillId);
                posting.Skills.Add(skill);
            }

            if (ModelState.IsValid)
            {
                company.Postings.Add(posting);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = posting.Id});
            }

            return View(posting);
        }

        // GET: Postings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }

        // POST: Postings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PostingDate,ClosingDate")] Posting posting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posting);
        }

        // GET: Postings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }

        // POST: Postings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posting posting = db.Postings.Find(id);
            db.Postings.Remove(posting);
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

        public ActionResult ApplyToPosting(string userId, int postingId)
        {
            var student = db.Students.Find(userId);
            //if user is not a student they get kicked back to the posting index
            if (student == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = student.Id;
            var posting = db.Postings.Include(po => po.Company).FirstOrDefault(x => x.Id == postingId);
            return View(posting);
            //this is the page to load up and make an email
        }
        [HttpPost]
        public ActionResult ApplyToPosting(string userId, int postingId, string coverLetter)
        {

            var student = db.Students.FirstOrDefault(stud => stud.Id == userId);
            
            Posting posting = db.Postings.Include(x => x.Company).FirstOrDefault(post => post.Id == postingId);

            string companyId = posting.Company.Id;

            Company company = db.Companies.Find(companyId);
            
            if(posting != null && student != null)
            {
                //send an email to the company email
                
                if (student.ResumeLink.Length > 0)
                {
                    MailMessage mail = new MailMessage("MITTinternships@MITT.ca", company.Email);
                    mail.Body = "A user has replied to your internship posting on MITT INTERN STUDENT PORTAL PATENT PENDING Check out their resume";

                    string routeToResume = String.Format(Server.MapPath("~") + "uploads\\{0}\\{1}\\{2}", student.Id, "resume", student.ResumeLink);
                    byte[] resumeBytes = System.IO.File.ReadAllBytes(routeToResume);

                    mail.Subject = "A new student has applied to your internship opportunity";
                    mail.To.Add(company.Email);
                    mail.To.Add("kyleelyk92@hotmail.com");

                    Attachment attachment = new Attachment(routeToResume);
                    mail.Attachments.Add(attachment);

                    //i don't have an smtp server
                    var credentials = new NetworkCredential("kyleelyk1992@gmail.com", "Starcraft2!");
                    // Smtp client
                    var smtp = new SmtpClient()
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Credentials = credentials
                    };
                    smtp.Send(mail);

                    if(!student.Postings.Contains(posting) == false)
                    {
                        student.Postings.Add(posting);
                    }
                    db.SaveChanges();
                }
            }
            MessageCarrier mc = new MessageCarrier() { actn = "StudentProfile", ctrller = "Students", UserId = student.Id, message = "Succesfully applied to the posting" };
            return RedirectToAction("MessagePage", "Home", mc);
        }
    }
}
