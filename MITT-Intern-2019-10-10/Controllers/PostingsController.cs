using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MITT_Intern_2019_10_10.Models;

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
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }

        // GET: Postings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Postings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,PostingDate,ClosingDate")] Posting posting)
        {
            if (ModelState.IsValid)
            {
                db.Postings.Add(posting);
                db.SaveChanges();
                return RedirectToAction("Index");
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
    }
}
