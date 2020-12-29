using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Uzytkownik
    {
        public enum role
        {
            Administrator,
            Admin,
            User
        }

        public int ID { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public role Rola { get; set; }
        public bool CzyBanowany { get; set; }
        public virtual ICollection<Serwer> Serwery { get; set; }
    }
}