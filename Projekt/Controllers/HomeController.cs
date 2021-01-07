using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projekt.ViewModels;
using Projekt.DAL;
using System.Net;
using System.IO;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class HomeController : Controller
    {
        private CsGoServerContext db = new CsGoServerContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<SerweryStatystyka> data = from serwer in db.Serwery
                                                   group serwer by serwer.DataUtworzenia into dateGroup
                                                   select new SerweryStatystyka()
                                                   {
                                                       DataUtworzenia = dateGroup.Key,
                                                       SerwerCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            string responseFromServer = "";
            WebRequest request = WebRequest.Create(
           "https://www.1shot1kill.pl/api_serwera?id=66710&action=status&pass=kojot123");
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
            ViewBag.Message = responseFromServer /*"Your contact page."*/;

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}