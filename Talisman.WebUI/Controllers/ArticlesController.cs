using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using Talisman.WebUI.Models;
using System.Web.UI;
using Talisman.WebUI;
using Talisman.Domain.Concrete;
using System.Threading.Tasks;

namespace Talisman.WebUI.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class ArticlesController : Controller
    {
        //private List<Article> Articles;
        private GData3 g;
        public ArticlesController()
        {
            g = new GData3();
            //Articles = g.Articles;
        }
        // GET: Articles
        //public ActionResult Index()
        //{
        //    return View();
        //}
        // [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        public async Task<ActionResult> Article(string artName=null)
        {
            int id;
            ArtMin ar; 
            if (artName != null&&artName!="")
            {
                ar = g.ArtMins.Where(a => a.Name.Replace(' ', '-').Replace("?", "").Replace(":", "").Replace("`", "").Replace(".", "") == artName).FirstOrDefault();
                if (ar == null)
                {
                    return new HttpNotFoundResult();
                }
                id = ar.Id;
            }
            else
            {                
                ar = g.ArtMins.FirstOrDefault();
                ViewBag.IsDef = true;
            }            
            ViewBag.id = ar.Id;
            var api=new ApiDb2();
            var cont = (await api.ReadArtCont(ar.Id)).Value as string;                       
            var m = new Article { Id = ar.Id, Name = ar.Name, Content = cont };            
            Response.Cache.SetLastModified(g.Get_LM(HeadersFor.Articles));
            return View(m);
        }

        //public ActionResult Article1()
        //{
        //    return View();
        //}
       //  [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        public PartialViewResult ArticleMenu(int id) 
        {
            //var Articles = (HttpContext.Application["GlobalData"] as GlobalData).Articles;
            var list = g.Articles.Select(a => new ArtMenuList { Id = a.Id, ArticleName = a.Name });
            var model = new ArticleMenuViewModel { CurrentId = id, List = list };
            return PartialView(model);
        }
         
    }
}