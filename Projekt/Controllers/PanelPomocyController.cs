using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Projekt.DAL;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class PanelPomocyController : Controller
    {
        private CsGoServerContext db = new CsGoServerContext();
        [Authorize(Roles = "Administrator,Admin")]
        // GET: PanelPomocy
        /*public ActionResult Index(string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            var panelPomocy = db.PanelPomocy.Include(p => p.Serwer);
            return View(panelPomocy.ToList());
        }*/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.StatusSortParm = String.IsNullOrEmpty(sortOrder) ? "status_zgloszenia_desc" : "";
            ViewBag.NickSortParm = sortOrder == "Nick" ? "nick_desc" : "Nick";
            ViewBag.NazwaSortParm = sortOrder == "NazwaSerwera" ? "nazwa_serwera_desc" : "NazwaSerwera";

            var panelPomocy = from p in db.PanelPomocy
                              select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                panelPomocy = panelPomocy.Where(p => p.Serwer.NazwaSerwera.Contains(searchString)
                                       || p.UzytkownikNazwa.Contains(searchString)
                                       || p.AktualnyStatus.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "status_zgloszenia_desc":
                    panelPomocy = panelPomocy.OrderByDescending(p => p.AktualnyStatus);
                    break;
                case "Nick":
                    panelPomocy = panelPomocy.OrderBy(p => p.UzytkownikNazwa);
                    break;
                case "nick_desc":
                    panelPomocy = panelPomocy.OrderByDescending(p => p.UzytkownikNazwa);
                    break;
                case "NazwaSerwera":
                    panelPomocy = panelPomocy.OrderBy(p => p.Serwer.NazwaSerwera);
                    break;
                case "nazwa_serwera_desc":
                    panelPomocy = panelPomocy.OrderByDescending(p => p.Serwer.NazwaSerwera);
                    break;
                default:
                    panelPomocy = panelPomocy.OrderBy(p => p.AktualnyStatus);
                    break;
            }
            return View(panelPomocy.ToList());
        }

        // GET: PanelPomocy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PanelPomoc panelPomoc = db.PanelPomocy.Find(id);
            if (panelPomoc == null)
            {
                return HttpNotFound();
            }
            return View(panelPomoc);
        }

        // GET: PanelPomocy/Create
        public ActionResult Create()
        {
            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera");
            return View();
        }

        // POST: PanelPomocy/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OpisProblemu")] PanelPomoc panelPomoc, int? id)
        {
            if (ModelState.IsValid)
            {
                Serwer serwer = db.Serwery.Find(id);
                panelPomoc.SerwerID = serwer.ID;
                panelPomoc.UzytkownikNazwa = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name).Nick;
                db.PanelPomocy.Add(panelPomoc);
                db.SaveChanges();
                ViewBag.Message = "Zgłoszenie wysłane";
                return RedirectToAction("../Serwery/Index");
            }

            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera", panelPomoc.SerwerID);
            return View(panelPomoc);
        }

        // GET: PanelPomocy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PanelPomoc panelPomoc = db.PanelPomocy.Find(id);
            if (panelPomoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera", panelPomoc.SerwerID);
            return View(panelPomoc);
        }

        // POST: PanelPomocy/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SerwerID,UzytkownikNazwa,OpisProblemu,AktualnyStatus")] PanelPomoc panelPomoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panelPomoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SerwerID = new SelectList(db.Serwery, "ID", "NazwaSerwera", panelPomoc.SerwerID);
            return View(panelPomoc);
        }

        // GET: PanelPomocy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PanelPomoc panelPomoc = db.PanelPomocy.Find(id);
            if (panelPomoc == null)
            {
                return HttpNotFound();
            }
            return View(panelPomoc);
        }

        // POST: PanelPomocy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PanelPomoc panelPomoc = db.PanelPomocy.Find(id);
            db.PanelPomocy.Remove(panelPomoc);
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
