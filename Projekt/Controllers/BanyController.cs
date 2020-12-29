using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt.DAL;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class BanyController : Controller
    {
        private CsGoServerContext db = new CsGoServerContext();

        [Authorize(Roles = "Administrator,Admin")]
        // GET: Bany
        public ActionResult Index()
        {
            var bany = db.Bany.Include(b => b.Serwer);
            return View(bany.ToList());
        }

        // GET: Bany/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bany.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            return View(ban);
        }

        // GET: Bany/Create
        public ActionResult Create()
        {
            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera");
            return View();
        }

        // POST: Bany/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SerwerID,Steam,NazwaGracza,PowodBana,CzasBana")] Ban ban)
        {
            if (ModelState.IsValid)
            {
                string nazwaGracza = Request["NazwaGracza"];

                var uzytkownicy = from u in db.Uzytkownicy where u.Nick == nazwaGracza
                                  select u;

                if(uzytkownicy.AsQueryable().Count() > 0)
                {
                    Uzytkownik uzytkownik = db.Uzytkownicy.Single(u => u.Nick == nazwaGracza);
                    uzytkownik.CzyBanowany = true;
                }


                ban.UzytkownikNazwa = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name).Nick;
                db.Bany.Add(ban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera", ban.SerwerID);
            return View(ban);
        }

        // GET: Bany/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bany.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera", ban.SerwerID);
            return View(ban);
        }

        // POST: Bany/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SerwerID,UzytkownikNazwa,Steam,NazwaGracza,PowodBana,CzasBana")] Ban ban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ban).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera", ban.SerwerID);
            return View(ban);
        }

        // GET: Bany/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bany.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            return View(ban);
        }

        // POST: Bany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ban ban = db.Bany.Find(id);
            db.Bany.Remove(ban);
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
