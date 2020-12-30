using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using Projekt.DAL;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class UzytkownicyController : Controller
    {
        private CsGoServerContext db = new CsGoServerContext();

        // GET: Uzytkownicy
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NickSortParm = String.IsNullOrEmpty(sortOrder) ? "nick_desc" : "";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var uzytkownicy = from s in db.Uzytkownicy
                           select s;

            var bany = from b in db.Bany
                       select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                uzytkownicy = uzytkownicy.Where(u => u.Nick.Contains(searchString)
                                       || u.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nick_desc":
                    uzytkownicy = uzytkownicy.OrderByDescending(s => s.Nick);
                    break;
                case "Email":
                    uzytkownicy = uzytkownicy.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    uzytkownicy = uzytkownicy.OrderByDescending(s => s.Email);
                    break;
                default:
                    uzytkownicy = uzytkownicy.OrderBy(s => s.Nick);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(uzytkownicy.ToPagedList(pageNumber, pageSize));
        }

        // GET: Uzytkownicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // GET: Uzytkownicy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uzytkownicy/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nick,Email")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                db.Uzytkownicy.Add(uzytkownik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uzytkownik);
        }

        // GET: Uzytkownicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Uzytkownicy/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nick,Email")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uzytkownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uzytkownik);
        }

        // GET: Uzytkownicy/Ustawienia/5
        public ActionResult Ustawienia(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);

            var userId = userManager.FindByName(uzytkownik.Email);
                
            IList<string> roleNames = userManager.GetRoles(userId.Id);

            ViewBag.Message = roleNames[0];

            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Uzytkownicy/Ustawienia/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ustawienia([Bind(Include = "ID,Nick,Email,Rola")] Uzytkownik uzytkownik)
        {

                if (uzytkownik == null)
                {
                    return HttpNotFound();
                }
                //var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var userManager =  new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var userId = userManager.FindByName(uzytkownik.Email);
                IList<string> roleNames = userManager.GetRoles(userId.Id);
                //ViewBag.Message = roleNames[0];
                userManager.RemoveFromRole(userId.Id, roleNames[0]);
                //userManager.RemoveFromRole(userId.Id, "Administrator");
                //userManager.AddToRole(userId.Id, "Admin");

                userManager.AddToRole(userId.Id, uzytkownik.Rola.ToString());

            db.Entry(uzytkownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: Uzytkownicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Uzytkownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uzytkownik uzytkownik = db.Uzytkownicy.Find(id);
            db.Uzytkownicy.Remove(uzytkownik);
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
