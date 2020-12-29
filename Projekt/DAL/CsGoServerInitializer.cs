using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Projekt.DAL
{
    public class CsGoServerInitializer :DropCreateDatabaseIfModelChanges<CsGoServerContext>
    {
        protected override void Seed(CsGoServerContext context)
        {
            var uzytkownicy = new List<Uzytkownik>
            {
                new Uzytkownik{ Nick = "Kevcio", Email = "kevcio@wp.pl",Rola = Uzytkownik.role.Administrator, CzyBanowany = false},
                new Uzytkownik{ Nick = "xZwiadowca", Email = "xZwiadowca@wp.pl",Rola = Uzytkownik.role.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "Senpuj", Email = "senpuj@wp.pl",Rola =Uzytkownik.role.Admin, CzyBanowany = false}
            };
            uzytkownicy.ForEach(u => context.Uzytkownicy.Add(u));
            context.SaveChanges();

            var serwery = new List<Serwer>
            {
                new Serwer{ Uzytkownik = uzytkownicy[0], NazwaSerwera = "Serwer1", IP = "145.239.237.98", Port = 27015, DataUtworzenia = DateTime.Now, HasloApi = "kojot123", IdSerwera = 66710 },
                new Serwer{ Uzytkownik = uzytkownicy[1], NazwaSerwera = "Serwer2", IP = "91.224.117.107", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[2], NazwaSerwera = "Serwer3", IP = "91.224.117.123", Port = 27015, DataUtworzenia = DateTime.Now}
            };
            serwery.ForEach(s => context.Serwery.Add(s));
            context.SaveChanges();

            var bany = new List<Ban>
            {
                new Ban{ UzytkownikNazwa = "Kevcio", Serwer = serwery[0], Steam = "STEAM_1:1:561962195", NazwaGracza = "nonhop", PowodBana = "COS", CzasBana = 120}
            };
            bany.ForEach(b => context.Bany.Add(b));
            context.SaveChanges();

            var zgloszenia = new List<PanelPomoc>
            {
                new PanelPomoc{ Serwer = serwery[0], UzytkownikNazwa = "Kevcio", OpisProblemu = "Coś nie działa", AktualnyStatus = PanelPomoc.Status.Oczekujące}
            };
            zgloszenia.ForEach(b => context.PanelPomocy.Add(b));
            context.SaveChanges();

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Administrator"));
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));


           
            var user = new ApplicationUser { UserName = "kevcio@wp.pl" };
            var user2 = new ApplicationUser { UserName = "xZwiadowca@wp.pl" };
            var user3 = new ApplicationUser { UserName = "senpuj@wp.pl" };
            string password = "kevcio";
            string password2 = "xzwiadowca";
            string password3 = "senpuj";
            userManager.Create(user, password);
            userManager.Create(user2, password2);
            userManager.Create(user3, password3);
            userManager.AddToRole(user.Id, "Administrator");
            userManager.AddToRole(user2.Id, "User");
            userManager.AddToRole(user3.Id, "Admin");



        }
    }
}