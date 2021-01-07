using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Ban
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa Serwera")]
        public int SerwerID{ get; set; }
        [Display(Name = "Admin")]
        public string UzytkownikNazwa { get; set; }
        [Display(Name = "Steam ID")]
        public string Steam { get; set; }
        [Display(Name = "Nazwa Zbanowanego")]
        public string NazwaGracza { get; set; }
        [Display(Name = "Powod Bana")]
        public string PowodBana { get; set; }
        [Display(Name = "Długość bana")]
        public int CzasBana { get; set; }
        public virtual Serwer Serwer { get; set; }
    }
}