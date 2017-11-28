using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talisman.WebUI.Models
{
    public class ArticleMenuViewModel
    {
        public int CurrentId { get; set; }
        public IEnumerable<ArtMenuList> List { get; set; }
    }

    public class ArtMenuList
    {
        public int Id { get; set; }
        public string ArticleName { get; set; }
    }
}