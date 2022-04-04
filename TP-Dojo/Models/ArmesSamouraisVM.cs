using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP_Dojo.Models
{
    public class ArmesSamouraisVM
    {
        public Samourai Samourai { get; set; }

        public List<SelectListItem> Armes { get; set; } = new List<SelectListItem>();

        public int? IdSelectedArme { get; set; }
    }
}