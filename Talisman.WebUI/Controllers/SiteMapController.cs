using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talisman.WebUI.Models;
using Talisman.Domain.Concrete;

namespace Talisman.WebUI.Controllers
{
    public class SiteMapController : Controller
    {
        private GData3 g;
        public SiteMapController()
        {
            g = new GData3();
        }
        public ActionResult Map()
        {
            //this.ViewEngineCollection[0].FindView(ControllerContext,"Index","",false).View.Render()
            //Url.Action()
            var model = new List<SiteMapViewModel>();
            //главная страница
            model.Add(new SiteMapViewModel
            {
                Changefreq = "daily",
                Lastmod = g.Get_LM(HeadersFor.Index),
                Priority = 1,
                Url = Url.Action("Index", "Home")
            });
            //категории
            foreach (var c in g.Categories.Where(c=>c.Visible))
            {
                model.Add(new SiteMapViewModel
                {
                    Url = Url.Action("Category", "Category", new { categName = c.CategoryName.Replace(" ", "-") }),
                    Changefreq = "daily",
                    Priority = 0.9,
                    Lastmod = g.Get_LM(HeadersFor.Category, c.CategoryId)
                });
            }
            //позиции
            foreach (var p in g.Tovars.Where(p1=>p1.Visible && (g.Categories.Where(c=>c.CategoryId==p1.CategoryId && c.Visible).Count()>0)))
            {
                model.Add(new SiteMapViewModel
                {
                    Url = Url.Action("Position", "Category", new
                    {
                        catName = g.Categories.Where(c => c.CategoryId == p.CategoryId).FirstOrDefault().CategoryName.Replace(" ", "-"),
                        id = p.TovarId
                    }),
                    Lastmod = g.Get_LM(HeadersFor.Position, p.TovarId),
                    Priority = 0.8,
                    Changefreq = "daily"
                });
            }
            //статьи
            foreach (var a in g.Articles)
            {
                model.Add(new SiteMapViewModel
                {
                    Url = Url.Action("Article", "Articles", new
                    {
                        artName = a.Name.Replace(' ', '-').Replace("?", "").Replace(":", "").Replace("`", "").Replace(".", "")
                    }),
                    Lastmod = g.Get_LM(HeadersFor.Articles),
                    Changefreq = "daily",
                    Priority = 0.6
                });
            }
            //услуги
            model.Add(new SiteMapViewModel
            {
                Url = Url.Action("List", "Services"),
                Lastmod = g.Get_LM(HeadersFor.Services),
                Priority = 0.6,
                Changefreq = "daily"
            });
            //контакты
            model.Add(new SiteMapViewModel
            {
                Url=Url.Action("Contacts","Home"),
                 Lastmod=g.Get_LM(HeadersFor.Contacts),
                  Changefreq="daily",
                   Priority=0.6
            });
            //отзывы
            model.Add(new SiteMapViewModel
            {
                 Url=Url.Action("List","FeedBack"),
                  Lastmod=g.Get_LM(HeadersFor.Feedbacks),
                   Changefreq="daily",
                    Priority=0.6
            });
            return View(model);
        }
    }
}