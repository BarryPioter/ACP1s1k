using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Projekt.Models
{
    public class Serwer
    {
        public int ID { get; set; }
        public int UzytkownikID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 3)]
        [Display(Name = "Nazwa Serwera")]
        public string NazwaSerwera { get; set; }
        //[RegularExpression(@"^\d([1-9]|[1-9][0-9]|[1-2][0-5][0-5]).\d([1-9]|[1-9][0-9]|[1-2][0-5][0-5]).\d([1-9]|[1-9][0-9]|[1-2][0-5][0-5]).\d([1-9]|[1-9][0-9]|[1-2][0-5][0-5])$", ErrorMessage = "IP musi składać sie z liczb od 1 do 255 i być odzielone kropkami ###.###.###.###")]
        [Required]
        public string IP { get; set; }
        [Required]
        public int Port { get; set; }
        [Display(Name = "Hasło Api")]
        public String HasloApi { get; set; }
        [Display(Name = "Id Serwera")]
        public int IdSerwera { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Utworzenia")]
        public DateTime DataUtworzenia { get; set; }
        [NotMapped]
        public String Status { get; set; }
        public string Obrazek { get; set; }
        public int MyProperty { get; set; }
        public virtual ICollection<Ban> Bany { get; set; }
        public virtual ICollection<PanelPomoc> PanelPomocy { get; set; }
        public virtual Uzytkownik Uzytkownik { get; set; }
    }
}