using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt.DAL;

namespace Projekt.Models
{
    public class SerweryController : Controller
    {
        private CsGoServerContext db = new CsGoServerContext();

        // GET: Serwery
        public ActionResult Index()
        {
            var serwery = db.Serwery.Include(s => s.Uzytkownik);
            return View(serwery.ToList());
        }

        // GET: Serwery/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serwer serwer = db.Serwery.Find(id);
            if (serwer == null)
            {
                return HttpNotFound();
            }
            return View(serwer);
        }

        // GET: Serwery/Create
        public ActionResult Create()
        {
            ViewBag.UzytkownikID = new SelectList(db.Uzytkownicy, "ID", "Nick");
            return View();
        }

        // POST: Serwery/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UzytkownikID,NazwaSerwera,IP,Port,DataUtworzenia")] Serwer serwer)
        {
            if (ModelState.IsValid)
            {
                db.Serwery.Add(serwer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UzytkownikID = new SelectList(db.Uzytkownicy, "ID", "Nick", serwer.UzytkownikID);
            return View(serwer);
        }

        // GET: Serwery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serwer serwer = db.Serwery.Find(id);
            if (serwer == null)
            {
                return HttpNotFound();
            }
            ViewBag.UzytkownikID = new SelectList(db.Uzytkownicy, "ID", "Nick", serwer.UzytkownikID);
            return View(serwer);
        }

        // POST: Serwery/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UzytkownikID,NazwaSerwera,IP,Port,DataUtworzenia")] Serwer serwer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serwer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UzytkownikID = new SelectList(db.Uzytkownicy, "ID", "Nick", serwer.UzytkownikID);
            return View(serwer);
        }

        // GET: Serwery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serwer serwer = db.Serwery.Find(id);
            if (serwer == null)
            {
                return HttpNotFound();
            }
            return View(serwer);
        }

        // POST: Serwery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Serwer serwer = db.Serwery.Find(id);
            db.Serwery.Remove(serwer);
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
