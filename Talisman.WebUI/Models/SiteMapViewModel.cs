using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talisman.WebUI.Models
{
    public class SiteMapViewModel
    {
        public string Url { get; set; }
        public DateTime Lastmod { get; set; }
        public double Priority { get; set; }
        public string Changefreq { get; set; }
    }
}