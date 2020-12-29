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
using System.Net;
using System.IO;
using Newtonsoft.Json;
//using RCONServerLib;
//PM> Install-Package source-rcon-server -Version 1.2.0
namespace Projekt.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using RCONServerLib;
    using System.CodeDom;
    using System.Web.Security;
    [Authorize]
    public class SerweryController : Controller
    {
        private CsGoServerContext db = new CsGoServerContext();

        //[Authorize(Roles = "Administrator,Admin")]
        // GET: Serwery
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = userManager.GetRoles(User.Identity.GetUserId());

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nazwa_serwera_desc" : "";
            ViewBag.NickSortParm = sortOrder == "Nick" ? "nick_desc" : "Nick";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var serwery = from s in db.Serwery
                          select s;



            if (roles[0] != "Administrator")
            {
                serwery = from s in db.Serwery
                              where s.Uzytkownik.Email == User.Identity.Name
                              select s;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                serwery = serwery.Where(s => s.NazwaSerwera.Contains(searchString)
                                       || s.IP.Contains(searchString)
                                       || s.Uzytkownik.Nick.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nazwa_serwera_desc":
                    serwery = serwery.OrderByDescending(s => s.NazwaSerwera);
                    break;
                case "Nick":
                    serwery = serwery.OrderBy(s => s.Uzytkownik.Nick);
                    break;
                case "nick_desc":
                    serwery = serwery.OrderByDescending(s => s.Uzytkownik.Nick);
                    break;
                case "Date":
                    serwery = serwery.OrderBy(s => s.DataUtworzenia);
                    break;
                case "date_desc":
                    serwery = serwery.OrderByDescending(s => s.DataUtworzenia);
                    break;
                default:
                    serwery = serwery.OrderBy(s => s.NazwaSerwera);
                    break;
            }
            return View(serwery.ToList());
        }

        public ActionResult Start(int? id)
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
            string responseFromServer = "";
            WebRequest request = WebRequest.Create("https://www.1shot1kill.pl/api_serwera?id=" + serwer.IdSerwera + "&action=start&pass=" + serwer.HasloApi + "");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }
            dynamic serverResponse = JsonConvert.DeserializeObject(responseFromServer);

            ViewBag.Message = serverResponse.desc/*"Your contact page."*/;

            return View();
        }
        public ActionResult Stop(int? id)
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
            string responseFromServer = "";
            WebRequest request = WebRequest.Create("https://www.1shot1kill.pl/api_serwera?id=" + serwer.IdSerwera + "&action=stop&pass=" + serwer.HasloApi + "");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            dynamic serverResponse = JsonConvert.DeserializeObject(responseFromServer);

            ViewBag.Message = serverResponse.desc/*"Your contact page."*/;

            return View();
        }
        public ActionResult Restart(int? id)
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
            string responseFromServer = "";
            WebRequest request = WebRequest.Create("https://www.1shot1kill.pl/api_serwera?id=" + serwer.IdSerwera + "&action=restart&pass=" + serwer.HasloApi + "");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            dynamic serverResponse = JsonConvert.DeserializeObject(responseFromServer);

            ViewBag.Message = serverResponse.desc/*"Your contact page."*/;

            return View();
        }
        // GET: Serwery/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serwer serwer = db.Serwery.Find(id);
            string responseFromServer = "";
            WebRequest request = WebRequest.Create("https://www.1shot1kill.pl/api_serwera?id="+serwer.IdSerwera+ "&action=status&pass="+serwer.HasloApi+"");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }
            dynamic serverResponse = JsonConvert.DeserializeObject(responseFromServer);
            serwer.Status = serverResponse.status;
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
        public ActionResult Create([Bind(Include = "ID,NazwaSerwera,IP,Port,HasloApi,IdSerwera")] Serwer serwer)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["plikZObrazkiem"];
                if(file != null && file.ContentLength > 0)
                {
                    serwer.Obrazek = System.Guid.NewGuid().ToString();
                    serwer.Obrazek = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Obrazki/") + serwer.Obrazek);
                }
                serwer.UzytkownikID = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name).ID;
                serwer.DataUtworzenia = DateTime.Now;
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
        public ActionResult Edit([Bind(Include = "ID,NazwaSerwera,IP,Port,HasloApi,IdSerwera")] Serwer serwer)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["plikZObrazkiem"];
                if (file != null && file.ContentLength > 0)
                {
                    //serwer.Obrazek = System.Guid.NewGuid().ToString();
                    serwer.Obrazek = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Obrazki/") + serwer.Obrazek);
                }
                serwer.UzytkownikID = db.Uzytkownicy.Single(u => u.Email == User.Identity.Name).ID;
                serwer.DataUtworzenia = DateTime.Now;
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
