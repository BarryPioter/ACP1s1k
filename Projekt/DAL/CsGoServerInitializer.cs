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
    public class CsGoServerInitializer : DropCreateDatabaseIfModelChanges<CsGoServerContext>
    {
        protected override void Seed(CsGoServerContext context)
        {
            var uzytkownicy = new List<Uzytkownik>
            {
                new Uzytkownik{ Nick = "Kevcio", Email = "kevcio@wp.pl",Rola = UzytkownikRola.Administrator, CzyBanowany = false},
                new Uzytkownik{ Nick = "xZwiadowca", Email = "xZwiadowca@wp.pl",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "Senpuj", Email = "senpuj@wp.pl",Rola =UzytkownikRola.Admin, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestAdmin1", Email = "TestAdmin1@a.a",Rola =UzytkownikRola.Admin, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestAdmin2", Email = "TestAdmin2@a.a",Rola =UzytkownikRola.Admin, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestAdmin3", Email = "TestAdmin3@a.a",Rola =UzytkownikRola.Admin, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser1", Email = "TestUser1@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser2", Email = "TestUser2@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser3", Email = "TestUser3@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser4", Email = "TestUser4@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser5", Email = "TestUser5@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser6", Email = "TestUser6@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser7", Email = "TestUser7@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser8", Email = "TestUser8@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser9", Email = "TestUser9@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser10", Email = "TestUser10@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser11", Email = "TestUser11@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser12", Email = "TestUser12@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser13", Email = "TestUser13@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
                new Uzytkownik{ Nick = "TestUser14", Email = "TestUser14@a.a",Rola = UzytkownikRola.User, CzyBanowany = false},
            };
            uzytkownicy.ForEach(u => context.Uzytkownicy.Add(u));
            context.SaveChanges();

            var serwery = new List<Serwer>
            {
                new Serwer{ Uzytkownik = uzytkownicy[0], NazwaSerwera = "Serwer1", IP = "145.239.237.98", Port = 27015, DataUtworzenia = DateTime.Now, HasloApi = "kojot123", IdSerwera = 66710 },
                new Serwer{ Uzytkownik = uzytkownicy[1], NazwaSerwera = "Serwer2", IP = "91.224.117.107", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[2], NazwaSerwera = "Serwer3", IP = "91.224.117.123", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[4], NazwaSerwera = "Serwer4", IP = "92.224.117.123", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[7], NazwaSerwera = "Serwer5", IP = "93.224.117.123", Port = 27013, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[8], NazwaSerwera = "Serwer6", IP = "94.224.117.123", Port = 27014, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[9], NazwaSerwera = "Serwer7", IP = "95.224.117.123", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[10], NazwaSerwera = "Serwer8", IP = "96.224.117.123", Port = 28015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[11], NazwaSerwera = "Serwer9", IP = "97.224.117.123", Port = 27016, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[12], NazwaSerwera = "Serwer10", IP = "98.224.117.123", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[13], NazwaSerwera = "Serwer11", IP = "99.224.117.123", Port = 21015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[14], NazwaSerwera = "Serwer12", IP = "91.224.116.123", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[6], NazwaSerwera = "Serwer13", IP = "91.224.115.123", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[3], NazwaSerwera = "Serwer14", IP = "91.224.114.123", Port = 22215, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[4], NazwaSerwera = "Serwer15", IP = "91.224.113.123", Port = 27015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[11], NazwaSerwera = "Serwer16", IP = "91.224.112.123", Port = 27045, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[15], NazwaSerwera = "Serwer17", IP = "91.224.111.123", Port = 26015, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[2], NazwaSerwera = "Serwer18", IP = "91.224.110.123", Port = 27025, DataUtworzenia = DateTime.Now},
                new Serwer{ Uzytkownik = uzytkownicy[0], NazwaSerwera = "Serwer19", IP = "91.224.117.122", Port = 27014, DataUtworzenia = DateTime.Now},
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
                new PanelPomoc{ Serwer = serwery[0], UzytkownikNazwa = "Kevcio", OpisProblemu = "Coś nie działa", AktualnyStatus = PanelPomoc.Status.Oczekujące},
                new PanelPomoc{ Serwer = serwery[3], UzytkownikNazwa = "Barry", OpisProblemu = "zepsuty", AktualnyStatus = PanelPomoc.Status.Oczekujące},
                new PanelPomoc{ Serwer = serwery[6], UzytkownikNazwa = "TestUser2", OpisProblemu = "cheater", AktualnyStatus = PanelPomoc.Status.Oczekujące},
                new PanelPomoc{ Serwer = serwery[1], UzytkownikNazwa = "TestUser5", OpisProblemu = "padl", AktualnyStatus = PanelPomoc.Status.Oczekujące},
                new PanelPomoc{ Serwer = serwery[2], UzytkownikNazwa = "TestUser9", OpisProblemu = "laggi", AktualnyStatus = PanelPomoc.Status.Oczekujące},
                new PanelPomoc{ Serwer = serwery[7], UzytkownikNazwa = "TestUser11", OpisProblemu = "lag", AktualnyStatus = PanelPomoc.Status.Oczekujące},
                new PanelPomoc{ Serwer = serwery[9], UzytkownikNazwa = "TestUser1", OpisProblemu = "laguje", AktualnyStatus = PanelPomoc.Status.Oczekujące},
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
            string password = "kevcio";
            userManager.Create(user, password);
            userManager.AddToRole(user.Id, "Administrator");

            var user2 = new ApplicationUser { UserName = "xZwiadowca@wp.pl" };
            string password2 = "xzwiadowca";
            userManager.Create(user2, password2);
            userManager.AddToRole(user2.Id, "User");

            var user3 = new ApplicationUser { UserName = "senpuj@wp.pl" };
            string password3 = "senpuj";
            userManager.Create(user3, password3);
            userManager.AddToRole(user3.Id, "Admin");

            var admin1 = new ApplicationUser { UserName = "TestAdmin1@a.a" };
            string adminpassword1 = "testadmin1";
            userManager.Create(admin1, adminpassword1);
            userManager.AddToRole(admin1.Id, "Admin");

            var admin2 = new ApplicationUser { UserName = "TestAdmin2@a.a" };
            string adminpassword2 = "testadmin2";
            userManager.Create(admin2, adminpassword2);
            userManager.AddToRole(admin2.Id, "Admin");

            var admin3 = new ApplicationUser { UserName = "TestAdmin3@a.a" };
            string adminpassword3 = "testadmin3";
            userManager.Create(admin3, adminpassword3);
            userManager.AddToRole(admin3.Id, "Admin");

            var testuser1 = new ApplicationUser { UserName = "TestUser1@a.a" };
            string userpassword1 = "testuser1";
            userManager.Create(testuser1, userpassword1);
            userManager.AddToRole(testuser1.Id, "User");

            var testuser2 = new ApplicationUser { UserName = "TestUser2@a.a" };
            string userpassword2 = "testuser2";
            userManager.Create(testuser2, userpassword2);
            userManager.AddToRole(testuser2.Id, "User");

            var testuser3 = new ApplicationUser { UserName = "TestUser3@a.a" };
            string userpassword3 = "testuser3";
            userManager.Create(testuser3, userpassword3);
            userManager.AddToRole(testuser3.Id, "User");

            var testuser4 = new ApplicationUser { UserName = "TestUser4@a.a" };
            string userpassword4 = "testuser4";
            userManager.Create(testuser4, userpassword4);
            userManager.AddToRole(testuser4.Id, "User");

            var testuser5 = new ApplicationUser { UserName = "TestUser5@a.a" };
            string userpassword5 = "testuser5";
            userManager.Create(testuser5, userpassword5);
            userManager.AddToRole(testuser5.Id, "User");

            var testuser6 = new ApplicationUser { UserName = "TestUser6@a.a" };
            string userpassword6 = "testuser6";
            userManager.Create(testuser6, userpassword6);
            userManager.AddToRole(testuser6.Id, "User");

            var testuser7 = new ApplicationUser { UserName = "TestUser7@a.a" };
            string userpassword7 = "testuser7";
            userManager.Create(testuser7, userpassword7);
            userManager.AddToRole(testuser7.Id, "User");

            var testuser8 = new ApplicationUser { UserName = "TestUser8@a.a" };
            string userpassword8 = "testuser8";
            userManager.Create(testuser8, userpassword8);
            userManager.AddToRole(testuser8.Id, "User");

            var testuser9 = new ApplicationUser { UserName = "TestUser9@a.a" };
            string userpassword9 = "testuser9";
            userManager.Create(testuser9, userpassword9);
            userManager.AddToRole(testuser9.Id, "User");

            var testuser10 = new ApplicationUser { UserName = "TestUser10@a.a" };
            string userpassword10 = "testuser10";
            userManager.Create(testuser10, userpassword10);
            userManager.AddToRole(testuser10.Id, "User");

            var testuser11 = new ApplicationUser { UserName = "TestUser11@a.a" };
            string userpassword11 = "testuser11";
            userManager.Create(testuser11, userpassword11);
            userManager.AddToRole(testuser11.Id, "User");

            var testuser12 = new ApplicationUser { UserName = "TestUser12@a.a" };
            string userpassword12 = "testuser12";
            userManager.Create(testuser12, userpassword12);
            userManager.AddToRole(testuser12.Id, "User");

            var testuser13 = new ApplicationUser { UserName = "TestUser13@a.a" };
            string userpassword13 = "testuser13";
            userManager.Create(testuser13, userpassword13);
            userManager.AddToRole(testuser13.Id, "User");

            var testuser14 = new ApplicationUser { UserName = "TestUser14@a.a" };
            string userpassword14 = "testuser14";
            userManager.Create(testuser14, userpassword14);
            userManager.AddToRole(testuser14.Id, "User");

        }
    }
}