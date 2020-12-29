using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.ViewModels
{
    public class SerweryStatystyka
    {
        [DataType(DataType.Date)]
        public DateTime? DataUtworzenia { get; set; }
        public int SerwerCount { get; set; }
    }
}