using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class PanelPomoc
    {
        
        public int ID { get; set; }

        [Display(Name = "Nazwa Użytkownika")]
        public string UzytkownikNazwa { get; set; }

        [Display(Name = "Opis problemu")]
        public string OpisProblemu { get; set; }

        [Display(Name = "Status zgłoszenia")]
        public Status AktualnyStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        public TypZgloszenia Typ { get; set; }

        public virtual Serwer Serwer { get; set; }

        public virtual Uzytkownik Admin { get; set; }
        public virtual Uzytkownik Uzytkownik { get; set; }

        public virtual ICollection<Wiadomosc> Wiadomosci { get; set; }

    }

    public enum Status
    {
        Oczekujące,
        [Display(Name = "W trakcie")]
        W_Trakcie,
        Zakończone,
        Anulowane
    }

    public enum TypZgloszenia
    {
        [Display(Name = "Problemy z serwerem")]
        Problemy_z_serwerem,
        [Display(Name = "Problemy z graczem")]
        Problemy_z_graczem,
        Inne
    }
}