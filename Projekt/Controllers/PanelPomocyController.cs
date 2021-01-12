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
        public ActionResult Index()
        {
            return View(db.PanelPomocy.ToList());
        }

        [Authorize]
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
                return RedirectToAction("Index", "Home");
            }

            Uzytkownik user = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name);
            if (user.ID != panelPomoc.Uzytkownik.ID && ((panelPomoc.Admin != null && panelPomoc.Admin.ID != user.ID) || (panelPomoc.Admin == null)))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(panelPomoc);
        }

        [Authorize]
        // GET: PanelPomocy/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SerwerID = id;
            return View();
        }

        // POST: PanelPomocy/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OpisProblemu,Typ,UzytkownikNazwa")] PanelPomoc panelPomoc)
        {
            if (ModelState.IsValid)
            {
                panelPomoc.Data = DateTime.Now;
                Serwer serwer = db.Serwery.Find(panelPomoc.ID);
                panelPomoc.Serwer = serwer;
                panelPomoc.Uzytkownik = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name);
                panelPomoc.AktualnyStatus = Status.Oczekujące;
                panelPomoc.Wiadomosci = new List<Wiadomosc>();
                db.PanelPomocy.Add(panelPomoc);
                db.SaveChanges();
                ViewBag.Message = "Zgłoszenie wysłane";
                return RedirectToAction("Index","Home");
            }

            ViewBag.SerwerID = panelPomoc.ID;
            return View(panelPomoc);
        }

        [Authorize(Roles = "Administrator,Admin")]
        public ActionResult Przyjmij(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PanelPomoc panelPomoc = db.PanelPomocy.Find(id);
            if (panelPomoc == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Uzytkownik admin = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name);
            if (admin.Rola == UzytkownikRola.User) {
                return RedirectToAction("Index", "Home");
            }

            panelPomoc.Admin = admin;
            panelPomoc.AktualnyStatus = Status.W_Trakcie;

            db.SaveChanges();

            return RedirectToAction("Details/" + panelPomoc.ID);
        }

        [Authorize(Roles = "Administrator,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ZmienStatus([Bind(Include = "ID,AktualnyStatus")] PanelPomoc panelPomoc)
        {
            PanelPomoc panel = db.PanelPomocy.Find(panelPomoc.ID);

            Uzytkownik admin = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name);
            if (admin.Rola == UzytkownikRola.User && panel.Admin.ID != admin.ID)
            {
                return RedirectToAction("Index");
            }

            panel.AktualnyStatus = panelPomoc.AktualnyStatus;

            Wiadomosc nowa = new Wiadomosc { Data = DateTime.Now, Tresc = "Zmiana statusu na "+ panel.AktualnyStatus, Uzytkownik = admin };

            panel.Wiadomosci.Add(nowa);
            db.Wiadomosci.Add(nowa);

            db.SaveChanges();

            return RedirectToAction("Details/" + panelPomoc.ID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NowaWiadomosc([Bind(Include = "ID,Tresc")] Wiadomosc wiadomosc)
        {

            PanelPomoc panelPomoc = db.PanelPomocy.Find(wiadomosc.ID);
            if (panelPomoc == null)
            {
                return RedirectToAction("Index", "Home");
            }

            

            Uzytkownik user = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name);
            if (user.ID != panelPomoc.Uzytkownik.ID && (panelPomoc.Admin != null && panelPomoc.Admin.ID != user.ID))
            {
                return RedirectToAction("Index", "Home");
            }

            Wiadomosc nowa = new Wiadomosc { Data = DateTime.Now, Tresc = wiadomosc.Tresc, Uzytkownik = user };

            panelPomoc.Wiadomosci.Add(nowa);
            db.Wiadomosci.Add(nowa);

            db.SaveChanges();

            return RedirectToAction("Details/" + panelPomoc.ID);
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
