using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class PanelPomoc
    {
        public enum Status
        {
            Oczekujące,
            W_Trakcie,
            Zakończone
        }
        public int ID { get; set; }
        [Display(Name = "Nazwa Serwera")]
        public int SerwerID { get; set; }
        [Display(Name = "Nazwa Użytkownika")]
        public string UzytkownikNazwa { get; set; }
        [Display(Name = "Opis problemu")]
        public string OpisProblemu { get; set; }
        [Display(Name = "Status zgłoszenia")]
        public Status AktualnyStatus { get; set; }
        public virtual Serwer Serwer { get; set; }
    }
}