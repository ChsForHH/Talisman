using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Talisman.WebUI.Models;
using Talisman.Domain.Entities;
using Talisman.Domain.Abstract;
using System.Web.UI;
using Talisman.WebUI;
using Talisman.WebUI.App_Start;
using Talisman.Domain.Concrete;
using System.Web.Helpers;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.IO;

namespace Talisman.WebUI.Controllers
{
    public class VData : IViewDataContainer
    {
        //public VData() { }
        public ViewDataDictionary ViewData { get; set; }

    }

    [SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class HomeController : Controller
    {
        
        private GData3 global;        
        public HomeController()
        {
            //var inst=HttpContext.ApplicationInstance.
            //new GData2().Init().Wait();
            global = new GData3();          
        }
       
       // [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]        
        public ActionResult Index(string p="")
        {
            if (p != "")
            {
                return new HttpNotFoundResult();
            }
            bool ok = false;
            var path = Server.MapPath("~/") + "Cach/Home/Index";
            try
            {                
                var cach = System.IO.File.ReadAllText(path);
                var d = System.IO.File.GetLastWriteTime(path);
                Response.Cache.SetLastModified(d);
                
                Response.Write(cach);
                ok = true;
            }
            catch (Exception exc)
            {
                try
                {
                    System.IO.File.AppendAllText(Server.MapPath("~/") + "ErrorsLog/Log.txt", exc.Message + "\n\t");
                }
                catch { }
            }
            if (!ok)
            {
                var model=new List<SliderViewModel>();
                foreach(var c in global.Categories.Where(cc=>cc.Visible)){
                    model.Add(new SliderViewModel{ CtegoryName=c.CategoryName, 
                        PhotoUrl="/Content/CategoryPhotos/category" + c.CategoryId.ToString() + "s0.jpeg"});
                }                            
                if (!(bool)HttpContext.Application["IsLocked"])
                {
                    Response.Cache.SetLastModified(global.Get_LM(HeadersFor.Index));
                }
                return View(model);
            }
            else
            {
                return new EmptyResult();
            }
        }
        public PartialViewResult CallBack()
        {
            return PartialView();
        }
        private Result SendEmail(CallBackViewModel cb)
        {
            Result r = null;
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "chsg73";
                WebMail.Password = "1972775qw";
                WebMail.From = "chsg73@gmail.com";
                WebMail.Send(global.Over.Email, "Запрос на обратный звонок",
                "Получена заявка на обратный звонок с сайта.<br/>" +
                "Имя: " + cb.Name + "<br/>" +
                "Номер: " + cb.Number
                );
                r = new Result(true);
            }
            catch (Exception exc)
            {
                r = new Result(false, exc.Message);
            }
            return r;
        }
        [HttpPost]
        public PartialViewResult CallBack(CallBackViewModel callback)
        {
            if (ModelState.IsValid)
            {
                var r = SendEmail(callback);
                if (r.Success)
                {
                    ViewBag.Ok = true;
                    return PartialView("CallBackOk",(object)"Запрос успешно отправлен!");
                }
                else
                {
                    ViewBag.Ok = false;
                    return PartialView("CallBackOk", (object)"Что-то пошло не так, повторите попытку.");
                }
            }
            return PartialView(callback);
        }

        public PartialViewResult CallBack2() 
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult CallBack2(CallBackViewModel callback) 
        {
            if (ModelState.IsValid)
            {
                var r = SendEmail(callback);
                if (r.Success)
                {
                    ViewBag.Ok = true;
                    return PartialView("CallBackOk", (object)"Запрос успешно отправлен!");
                }
                else
                {
                    ViewBag.Ok = false;
                    return PartialView("CallBackOk", (object)"Что-то пошло не так, повторите попытку.");
                }
            }
            return PartialView(callback);
        }
      //   [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        public PartialViewResult LeftNav()
        {           
            var Categories = global.Categories;
            return PartialView(Categories.Where(c=>c.Visible));
        }
       //  [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        public ViewResult Contacts()
        {
            Response.Cache.SetLastModified(global.Get_LM(HeadersFor.Contacts));
            return View();
        }
       //  [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        public PartialViewResult NewTovars()
        {
            
            var tap = global.TaP.Where(t=>t.IsNew).OrderBy(t=>t.CategoryId);
            var model = new TovarsAndPhotoViewModel { Categories = global.Categories.Where(c => c.Visible), TaP = tap };        
            return PartialView(model);
        }
         //[OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        //public JsonResult Viewer(int tovarId)
        //{
            
        //    var Tovars = global.Tovars.Where(t => t.IsNew && t.Visible).OrderBy(t => t.CategoryId);
        //    var Photos = global.Photos;
        //    var tovars = Tovars.Select(t => new Viewer
        //    {
        //        Tovar = t,
        //        PhotoIds = Photos.Where(p => p.TovarId == t.TovarId).Select(p => p.PhotoId)
        //    });
        //    var model = new ViewerViewModel { Viewers = tovars, CurrentTovarId = tovarId };

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
         
       //  [OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
         public PartialViewResult ArticleMenu2()
         {             
             var Articles = global.ArtMins;
             var list = Articles.Select(a => new ArtMenuList { Id = a.Id, ArticleName = a.Name });
             var model = new ArticleMenuViewModel { List = list };
             return PartialView(model);
         }
         //public class VData : IViewDataContainer
         //{
         //    //public VData() { }
         //    public ViewDataDictionary ViewData { get; set; }

         //}
         //public ActionResult List()
         //{
         //    var v = this.Index();
         //    var dc = this.ViewData;
         //    VData vd = new VData { ViewData = dc };
         //    var h2 = new HtmlHelper(new ViewContext(ControllerContext, v.View, null, null, null), vd);
         //    return View();
         //}
         private void SaveCach(string ControllerName, string ActionName, string path, object obj)
         {
            // var cach = this.Response.Cache;
            // var cc = this.Response.;
             
             var dc = this.ViewData;
             VData vd = new VData { ViewData = dc };
             var v = new RazorView(ControllerContext, "!", null, false, null);
             //var ccc = new ControllerContext();
             StringWriter sw = new StringWriter();
             var wc = new ViewContext();
             
             v.Render(new ViewContext(), sw);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
             var tw = Response.Output;
             var h2 = new HtmlHelper(new ViewContext(ControllerContext, v, ViewData, TempData, tw), vd);
             var htmlText = h2.Action(ActionName, ControllerName, obj).ToString();
             string path_ = Server.MapPath("~/") + path;
             try
             {
                 System.IO.File.WriteAllText(path_, htmlText);
             }
             catch
             {

             }
         }
        //private string SaveCache(){ 
        //    var dc = this.ViewData;
        //    VData vd = new VData { ViewData = dc };
        //    var v = new RazorView(ControllerContext, "!", null, false, null);
        //    var tw = Response.Output;
        //    var qq = Response.Cache;
            
        //    var h2 = new HtmlHelper(new ViewContext(ControllerContext, v, ViewData, TempData, tw), vd);
        //    //var h2 = new HtmlHelper(new ViewContext(), vd);
        //    var str = h2.Action("Category", "Category", new { categoryId=1}).ToString();
        //    return str;
        //}
         //public ActionResult Plugin()
         //{
         //    //var v = this.Contacts();
             
         //    return View((object)CachHtml());
         //}
        //[HttpPost]
        //public void Ceep(string p)
        //{
        //    double v = 0;
        //    double.TryParse(p, out v);
        //}
        //public ActionResult Plugin()
        //{
        //    return View();
        //}
    }
}