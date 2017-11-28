using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Talisman.Domain.Entities;

namespace Talisman.WebUI.Models
{
    public class TovarsAndPhotoViewModel
    {
        public IEnumerable<TovarsAndPhoto> TaP { get; set; }
        public IEnumerable<Categorie> Categories { get; set; }
    }
}