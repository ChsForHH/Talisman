using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using Talisman.WebUI.Models;
using System.Web.UI;
using Talisman.Domain.Concrete;
using System.Threading.Tasks;

namespace Talisman.WebUI.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class CategoryController : Controller
    {
        private List<Tovar> Tovars;
        private List<Categorie> Categories;
        private List<Photo> Photos;
        private List<Article> Articles;
        private List<TovarsAndPhoto> TaP;
        private GData3 g;
        public CategoryController()
        {
            g = new GData3();
            Tovars = g.Tovars;
            Categories = g.Categories;
            Photos = g.Photos;
            Articles = g.Articles;
            TaP = g.TaP;
        }
        //[OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        public ActionResult Category(string categName)
        {            
            var category = Categories.Where(c => c.CategoryName.Replace(" ","-").ToLower() == categName.ToLower() && c.Visible).FirstOrDefault();
            if (category == null)
            {
                return new HttpNotFoundResult();
            }
            bool ok = false;
            var path = Server.MapPath("~/") + "Cach/Category/Category"+categName;
            try
            {
                var cach = System.IO.File.ReadAllText(path);
                var d = System.IO.File.GetLastWriteTime(path);
                Response.Cache.SetLastModified(d);
                Response.Write(cach);
                ok = true;
            }
            catch(Exception exc)
            {
                try
                {
                    System.IO.File.AppendAllText(Server.MapPath("~/") + "ErrorsLog/Log.txt", exc.Message + "\n\t");
                }
                catch { }
            }
            if (!ok)
            {
                var tc = TaP.Where(t => t.CategoryId == category.CategoryId).OrderBy(t => t.TovarId);
                CategoryViewModel model = new CategoryViewModel { Category = category, Tovars = tc };

                if (!(bool)HttpContext.Application["IsLocked"])
                {
                    Response.Cache.SetLastModified(g.Get_LM(HeadersFor.Category, category.CategoryId));
                }
                return View(model);                
            }
            else
            {
                return new EmptyResult();
            }            
        }

        public async Task<ActionResult> Position(string catName, int id)
        {
            var cat = Categories.Where(c => c.CategoryName.Replace(" ", "-").ToLower() == catName.ToLower() && c.Visible).FirstOrDefault();
            if (cat == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                if (Tovars.Where(t => t.CategoryId == cat.CategoryId && t.TovarId == id && t.Visible).Count() == 0)
                {
                    return new HttpNotFoundResult();
                }
            }
            var api = new ApiDb2();
            var r1 = await api.ReadTovDesc(id);
            string desc = null;
            string ceo = null;
            string title = null;
            string keywords = null;
            IEnumerable<int> pIds = null;
            if (r1.Success)
            {
                var r2 = await api.ReadPhotoIds(id);
                if (r2.Success)
                {
                    var res = r1.Value as string[];
                    desc = res[0];
                    ceo = res[1];
                    title = res[2];
                    keywords = res[3];
                    pIds = r2.Value as IEnumerable<int>;
                }
                else
                {
                    return View("Error", (object)r2.Errormessage);
                }
            }
            else
            {
                return View("Error", (object)r1.Errormessage);
            }
            var tov = TaP.Where(t => t.TovarId == id).FirstOrDefault();
            var model = new Viewer { Tovar = new Tovar { TovarId = tov.TovarId, Name = tov.Name, Describing = desc, CategoryId = tov.CategoryId, CEO = ceo, Title = title, KeyWords = keywords }, PhotoIds = pIds, CategoryName = cat.CategoryName };
            Response.Cache.SetLastModified(g.Get_LM(HeadersFor.Position, tov.TovarId));
            var torg = cat.Article.Split('|');
            if (torg.Length == 3)
            {
                ViewBag.Torg = torg[2];
            }
            return View(model);
        }

        
       //  [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        //public JsonResult Viewer(int tovarId)
        //{
        //    //var Categories = (HttpContext.Application["GlobalData"] as GlobalData).Categories;
        //    //var Tovars = (HttpContext.Application["GlobalData"] as GlobalData).Tovars;
        //    //var Photos = (HttpContext.Application["GlobalData"] as GlobalData).Photos;
        //    var ct = Tovars.Where(t => t.TovarId == tovarId).Select(t => t.CategoryId).FirstOrDefault();
        //    //var viewers = Tovars.Where(t => t.CategoryId == ct && t.Visible).Select(t => new Viewer
        //    //{
        //    //    Tovar = t,
        //    //    PhotoIds = Photos.Where(p => p.TovarId == t.TovarId).Select(p => p.PhotoId)
        //    //});
        //    //var model = new ViewerViewModel { Viewers = viewers, CurrentTovarId = tovarId };

        //    //var category = Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
        //    var tov = Tovars.Where(t => t.CategoryId == ct && t.Visible).ToArray();
        //    var ph = Photos.Select(p => new { p.TovarId, p.PhotoId }).ToArray();

        //    var tovars = from t in tov.Where(to => to.CategoryId == ct)
        //                 join p in ph
        //                 on t.TovarId equals p.TovarId
        //                 select new { t, p } into temp
        //                 group temp by temp.t.TovarId into g
        //                 select new Viewer { Tovar = g.Select(x => x.t).FirstOrDefault(), PhotoIds = g.Select(np => np.p.PhotoId) };
        //    var model = new ViewerViewModel { Viewers = tovars, CurrentTovarId = tovarId };
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
    }
}