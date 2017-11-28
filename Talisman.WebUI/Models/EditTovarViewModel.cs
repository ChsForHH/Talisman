using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Talisman.Domain.Entities;

namespace Talisman.WebUI.Models
{
    public class EditTovarViewModel
    {
        public Tovar Tovar { get; set; }
        public int PhotoId { get; set; }
    }
}