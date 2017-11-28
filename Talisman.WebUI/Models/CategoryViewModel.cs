using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Talisman.Domain.Entities;


namespace Talisman.WebUI.Models
{
    

    public class Viewer
    {
        public Tovar Tovar { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<int> PhotoIds { get; set; }
    }
    public class ViewerViewModel
    {
        public IEnumerable<Viewer> Viewers { get; set; }
        public int CurrentTovarId { get; set; }
    }

    public class CategoryViewModel
    {
        public Categorie Category { get; set; }
        public IEnumerable<TovarsAndPhoto> Tovars { get; set; }
    }

    
}