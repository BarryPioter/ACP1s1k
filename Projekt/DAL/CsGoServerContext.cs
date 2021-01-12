using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Projekt.DAL
{
    public class CsGoServerContext : DbContext
    {
        public CsGoServerContext() 
            :base("DefaultConnection")
        { 
        }
        
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Serwer> Serwery { get; set; }
        public DbSet<Ban> Bany { get; set; }
        public DbSet<PanelPomoc> PanelPomocy { get; set; }
        public DbSet<Wiadomosc> Wiadomosci { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<PanelPomoc>().HasRequired(pj => pj.Serwer).WithMany().WillCascadeOnDelete(true);
            modelBuilder.Entity<Ban>().HasRequired(pj => pj.Serwer).WithMany().WillCascadeOnDelete(true);
        }
    }
}