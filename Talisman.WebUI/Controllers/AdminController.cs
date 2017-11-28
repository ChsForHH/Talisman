using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using Talisman.WebUI.Models;
using System.Web.Security;
using Talisman.Domain.Concrete;
using System.IO;
using System.Web.Mvc.Html;
using System.Drawing;
using System.Drawing.Imaging;


namespace Talisman.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private List<Tovar> Tovars1;
        private List<Categorie> Categories;
        private List<Photo> Photos1;
        private List<Article> Articles;
        private List<FeedBack> FeedBacks;
        private List<Service> Services1;
        private List<New> News1;
        private Over Over1;
        private NewRepository nrepo;
        private GData3 g;
        //IRepository repo;
        public AdminController()
        {
            nrepo = new NewRepository();
            g = new GData3();
            Tovars1 = g.Tovars;
            Categories = g.Categories;
            Photos1 = g.Photos;
            Articles = g.Articles;
            //repo = r;
            FeedBacks = g.FeedBacks;
            Services1 = g.Services;
            News1 = g.News;
            Over1 = g.Over;
        }


        public ActionResult ListCategories()
        {
            //var Categories = (HttpContext.Application["GlobalData"] as GlobalData).Categories;
            var model = Categories;
            return View(model);
        }

        public ActionResult EditCategory(int CategoryId)
        {
            //var Categories = (HttpContext.Application["GlobalData"] as GlobalData).Categories;
            var model = Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault();
            if (model == null)
            {
                return View("AdminError");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditCategory(Categorie category)
        {
            if (ModelState.IsValid)
            {
                category.LM = DateTime.Now;
                var result = await nrepo.Update<Categorie>(category);//repo.SaveCategory(category);
                if (result.Success)
                {
                    TempData["messageCreateOk"] = "Изменения успешно сохранены.";
                    return RedirectToAction("ListCategories");
                }
                else
                {
                    return View("AdminErr", result.Errormessage);
                }

            }
            return View(category);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory(Categorie category)
        {
            if (ModelState.IsValid)
            {
                category.CategoryId = 0;
                category.LM = DateTime.Now;                
                var result = await nrepo.Insert<Categorie>(category);
                if (result.Success)
                {
                    TempData["messageCreateOk"] = "Категория успешно создана.";
                    return RedirectToAction("ListCategories");
                }
                else
                {
                    return View("AdminErr", (object)result.Errormessage);
                }

            }
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {

            if (categoryId > 0)
            {
                var cou = Tovars1.Where(c => c.CategoryId == categoryId).Count();
                if (cou == 0)
                {
                    var r = await nrepo.Delete<Categorie>(categoryId);//repo.DeleteCategory(categoryId);
                    if (!r.Success)
                    {
                        TempData["messageNotDel"] = r.Errormessage;
                    }
                    else
                    {
                        TempData["messageOkDel"] = "Категория успешно удалена";
                    }
                }
                else
                {
                    TempData["messageNotDel"] = "Нельзя удалить категорию, содержащую позиции ассортимента!";
                }
            }
            return RedirectToAction("ListCategories");
        }

        public PartialViewResult Tovars(int categoryId)
        {
            ViewBag.categoryId = categoryId;
            //var Tovars = (HttpContext.Application["GlobalData"] as GlobalData).Tovars;
            //var Photos = (HttpContext.Application["GlobalData"] as GlobalData).Photos;
            var tov = Tovars1.Where(t => t.CategoryId == categoryId).ToArray();
            var ph = Photos1.Select(p => new { p.TovarId, p.PhotoId }).ToArray();
            var t1 = tov.Select(t => t.TovarId);
            var t2 = ph.Select(p => p.TovarId);
            var ex = t1.Except(t2);
            //var ph=repo.Photos.Ex
            //var model = tov.Select(t => new EditTovarViewModel { Tovar = t, 
            //    //PhotoId = 33
            //    PhotoId= ph.Where(p => p.TovarId == t.TovarId).Select(p=>p.PhotoId).FirstOrDefault() 
            //});
            var model = from t in tov.Where(to => to.CategoryId == categoryId)
                        join p in ph
                        on t.TovarId equals p.TovarId
                        select new { t, p } into temp
                        group temp by temp.t.TovarId into g
                        select new EditTovarViewModel { Tovar = g.Select(x => x.t).FirstOrDefault(), PhotoId = g.Min(y => y.p.PhotoId) };
            //var np=model.Except()

            var m = model.Union(ex.Select(e => new EditTovarViewModel { Tovar = tov.Where(t => t.TovarId == e).FirstOrDefault(), PhotoId = 0 }));
            ViewBag.category = Categories.Where(c => c.CategoryId == categoryId).Select(c => c.CategoryName).FirstOrDefault();
            return PartialView(m);
        }

        public async Task<ActionResult> CreateTovar(int categoryId)
        {
            int id = 1;
            //var Categories = (HttpContext.Application["GlobalData"] as GlobalData).Categories;
            var catName = Categories.Where(c => c.CategoryId == categoryId).Select(c => c.CategoryName).FirstOrDefault();
            if (Tovars1.Count() > 0)
            {
                id = (Tovars1.Select(t => t.TovarId).Max()) + 1;
            }

            Tovar tovar = new Tovar { TovarId = 0, CategoryId = categoryId, Name = catName + " модель" + id.ToString(), Visible = false, Describing = "", Price = 0, CEO = "", IsNew = false, LM = DateTime.Now };
            var r = await nrepo.Insert<Tovar>(tovar);//repo.SaveTovar(tovar);
            if (r.Success)
            {
                tovar.TovarId = (int)r.Value;
                return View(tovar);
            }
            else
            {
                return View("AdminErr", (object)r.Errormessage);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTovar(Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                //var r = await repo.SaveTovar(tovar);
                if (tovar.Describing == null)
                {
                    tovar.Describing = string.Empty;
                }
                if (tovar.Price == null)
                {
                    tovar.Price = 0;
                }
                tovar.LM = DateTime.Now;
                Result r = null;
                if (tovar.TovarId == 0)
                {
                    r = await nrepo.Insert<Tovar>(tovar);
                }
                else
                {
                    r = await nrepo.Update<Tovar>(tovar);
                }
                if (r.Success)
                {
                    TempData["messageOk"] = "Данные успешно сохранены.";
                }
                else
                {
                    TempData["messageNotOk"] = r.Errormessage;
                }

                return RedirectToAction("EditCategory", new { categoryId = tovar.CategoryId });
            }
            return View(tovar);
        }

        public ActionResult EditTovar(int tovarId)
        {
            // var Tovars = (HttpContext.Application["GlobalData"] as GlobalData).Tovars;
            var tovar = Tovars1.Where(t => t.TovarId == tovarId).FirstOrDefault();
            return View("CreateTovar", tovar);
        }

        public async Task<ActionResult> DeleteTovar(int tovarId, int categoryId)
        {
            var r = await nrepo.Delete<Tovar>(tovarId);//await repo.DeleteTovar(tovarId);
            if (r.Success)
            {
                //g.Tovars = null;
                return RedirectToAction("EditCategory", new { categoryId = categoryId });
            }
            else
            {
                return View("AdminErr");
            }

        }

        public PartialViewResult AddPhoto(int tovarId)
        {
            return PartialView((object)tovarId);
        }
        [HttpPost]
        public async Task<ActionResult> AddPhoto(int tovarId, IEnumerable<HttpPostedFileBase> images = null)
        {
            if (ModelState.IsValid)
            {
                // Photo photo = new Photo { TovarId = tovarId };
                if (images != null)
                {
                    foreach (var i in images)
                    {
                        var photo = new Photo2 { TovarId = tovarId };
                        photo.ImageMimeType = i.ContentType;
                        photo.Image = new byte[i.ContentLength];
                        photo.LM = DateTime.Now;
                        //photo.Mini=                        
                        i.InputStream.Read(photo.Image, 0, i.ContentLength);
                        var r = await nrepo.Insert<Photo2>(photo);//repo.SavePhoto(photo);
                        if (!r.Success)
                        {
                            return View("AdminErr", (object)r.Errormessage);
                        }
                        var tov = Tovars1.Where(t => t.TovarId == tovarId).FirstOrDefault();
                        tov.LM = photo.LM;
                        var r2 = await nrepo.Update<Tovar>(tov);
                        if (!r2.Success)
                        {
                            return View("AdminErr", (object)r2.Errormessage);
                        }
                        photo.PhotoId = (int)r.Value;
                        var s = photo.ImageMimeType.Replace("image/", "");
                        i.SaveAs(Server.MapPath("~/") + "/Content/TovarPhotos/" + photo.PhotoId.ToString() + "." + s);
                        ResizeImage(0.5, Server.MapPath("~/") + "/Content/TovarPhotos/" + photo.PhotoId.ToString() + "." + s,
                           Server.MapPath("~/") + "/Content/TovarPhotos/" + photo.PhotoId.ToString() + "s1" + "." + s);
                        ResizeImage(0.25, Server.MapPath("~/") + "/Content/TovarPhotos/" + photo.PhotoId.ToString() + "." + s,
                           Server.MapPath("~/") + "/Content/TovarPhotos/" + photo.PhotoId.ToString() + "s2" + "." + s);
                    }

                }
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //int categoryId = repo.Tovars.Where(t => t.TovarId == tovarId).Select(t=>t.CategoryId).FirstOrDefault();
                //TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("EditTovar", new { tovarId = tovarId });
            }
            else
            {
                // there is something wrong with the data values
                return PartialView(tovarId);
            }
        }
        public PartialViewResult AddCategoryPhoto(int categoryId)
        {
            return PartialView((object)categoryId);
        }
        [HttpPost]
        public ActionResult AddCategoryPhoto(int categoryId, HttpPostedFileBase image = null) 
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var s = image.ContentType.Replace("image/", "");
                    image.SaveAs(Server.MapPath("~/") + "/Content/CategoryPhotos/category" + categoryId.ToString() + "s0." + s);
                    ResizeImage(0.5, Server.MapPath("~/") + "/Content/CategoryPhotos/category" + categoryId.ToString() + "s0." + s,
                       Server.MapPath("~/") + "/Content/CategoryPhotos/category" + categoryId.ToString() + "s1" + "." + s);
                    ResizeImage(0.25, Server.MapPath("~/") + "/Content/CategoryPhotos/category" + categoryId.ToString() + "s0." + s,
                       Server.MapPath("~/") + "/Content/CategoryPhotos/category" + categoryId.ToString() + "s2" + "." + s);
                }                
                return RedirectToAction("EditCategory", new { CategoryId = categoryId }); 
            }
            else
            {               
                return PartialView(categoryId);
            }
        }

        public PartialViewResult Photos(int tovarId)
        {
            // var Photos = (HttpContext.Application["GlobalData"] as GlobalData).Photos;
            var model = Photos1.Where(p => p.TovarId == tovarId).Select(p => p.PhotoId);
            return PartialView(model);
        }

        public PartialViewResult PhotosMini(int tovarId)
        {
            //var Photos = (HttpContext.Application["GlobalData"] as GlobalData).Photos;
            var model = Photos1.Where(p => p.TovarId == tovarId).Select(p => p.PhotoId).FirstOrDefault();
            //if (model == null) model = 0;
            return PartialView(model);
        }

        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            //var Photos = (HttpContext.Application["GlobalData"] as GlobalData).Photos;
            var tovarId = Photos1.Where(p => p.PhotoId == photoId).Select(p => p.TovarId).FirstOrDefault();
            var r = await nrepo.Delete<Photo>(photoId);//repo.DeletePhoto(photoId);
            if (r.Success)
            {
                var tov = Tovars1.Where(t => t.TovarId == tovarId).FirstOrDefault();
                tov.LM = DateTime.Now;
                var r2 = await nrepo.Update<Tovar>(tov);
                if (!r2.Success)
                {
                    return View("AdminErr", (object)r2.Errormessage);
                }
                return RedirectToAction("EditTovar", new { tovarId = tovarId });
            }
            else
            {
                return View("AdminErr", (object)r.Errormessage);
            }

        }



        public ActionResult ArticlesAdmin()
        {
            var model = Articles;
            return View(model);
        }

        public ActionResult CreateArticle()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                article.Id = 0;
                article.Date = DateTime.Now;
                article.Author = "Admin";
                var r = await nrepo.Insert<Article>(article);//repo.SaveArticle(article);
                if (r.Success)
                {
                    TempData["messageCreateOk"] = "Статья успешно добавлена.";
                }
                else //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    TempData["messageCreateOk"] = r.Errormessage;
                }
                return RedirectToAction("ArticlesAdmin");
            }
            return View(article);
        }

        public ActionResult EditArticle(int articleId)
        {
            var model = Articles.Where(a => a.Id == articleId).FirstOrDefault();
            if (model == null)
            {
                return View("AdminError");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                var r = await nrepo.Update<Article>(article);//repo.SaveArticle(article);
                if (r.Success)
                {
                    TempData["messageCreateOk"] = "Изменения успешно сохранены.";
                }
                else
                {
                    TempData["messageCreateOk"] = r.Errormessage;
                }
                return RedirectToAction("ArticlesAdmin");
            }
            return View(article);
        }

        public ActionResult DepPhotoView()
        {
            return View();
        }
        //public ActionResult DepPhotos()
        //{
        //    string res = "";
        //    var photos = repo.Photos.ToArray();
        //    try
        //    {
        //        foreach (var p in photos)
        //        {
        //            string path = Server.MapPath("~/") + "/Content/TovarPhotos/" + p.PhotoId.ToString() + "."
        //                + p.ImageMimeType.Replace("image/", "");
        //            System.IO.File.WriteAllBytes(path, p.Image);
        //        }
        //        res = "Фото развернуты";
        //    }
        //    catch (Exception exc)
        //    {
        //        res = exc.Message;
        //    }
        //    TempData["res"] = res;
        //    return View("DepPhotoView");
        //}

        public ActionResult FeedBack()
        {
            var model = FeedBacks;
            return View(model);
        }

        public async Task<ActionResult> DeleteFeedBack(int id)
        {
            var r = await nrepo.Delete<FeedBack>(id);//repo.DeleteFeedBack(id);
            if (r.Success)
            {
                return RedirectToAction("Feedback");
            }
            else return View("AdminErr", (object)r.Errormessage);
        }
        public ActionResult EditFeedBack(int id)
        {
            var f = FeedBacks.Where(fb => fb.Id == id).FirstOrDefault();
            return View(f);
        }
        [HttpPost]
        public async Task<ActionResult> EditFeedBack(FeedBack f)
        {
            if (ModelState.IsValid)
            {
                //var r = await repo.SaveFeedBack(f);
                Result r = null;
                if (f.Id == 0)
                {
                    r = await nrepo.Insert<FeedBack>(f);
                }
                else
                {
                    r = await nrepo.Update<FeedBack>(f);
                }
                if (r.Success)
                {
                    TempData["mess"] = "Изменения успешно сохранены";
                    TempData["cl"] = "fbOk";
                    return RedirectToAction("FeedBack");
                }
                else
                {
                    TempData["mess"] = r.Errormessage;
                    TempData["cl"] = "fbErr";
                    return RedirectToAction("FeedBack");
                }
            }
            return View(f);
        }

        public ActionResult Services()
        {
            var model = Services1;
            return View(model);
        }
        public ActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateService(Service service)
        {
            if (ModelState.IsValid)
            {
                service.Id = 0;
                service.LM = DateTime.Now;
                var r = await nrepo.Insert<Service>(service);//repo.SaveService(service);
                if (r.Success)
                {
                    TempData["messageCreateOk"] = "Услуга успешно добавлена.";
                }
                else //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    TempData["messageCreateOk"] = r.Errormessage;
                }
                return RedirectToAction("Services");
            }
            return View(service);
        }

        public ActionResult EditService(int id)
        {
            var model = Services1.Where(a => a.Id == id).FirstOrDefault();
            if (model == null)
            {
                return View("AdminErr", (object)"Услуга не найдена!");
            }
            return View(model);
        }

        [HttpPost]
        //[AllowHtml]
        public async Task<ActionResult> EditService(Service service)
        {
            if (ModelState.IsValid)
            {
                service.LM = DateTime.Now;
                var r = await nrepo.Update<Service>(service);//repo.SaveService(service);
                if (r.Success)
                {
                    TempData["messageCreateOk"] = "Изменения успешно сохранены.";
                }
                else
                {
                    TempData["messageCreateOk"] = r.Errormessage;
                }
                return RedirectToAction("Services");
            }
            return View(service);
        }

        /*-----------------------Новости--------------------*/
        public ActionResult News()
        {
            var model = News1;
            return View(model);
        }
        public ActionResult CreateNew()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateNew(New new_)
        {
            if (ModelState.IsValid)
            {
                new_.Id = 0;

                var r = await nrepo.Insert<New>(new_);//repo.SaveNew(new_);
                if (r.Success)
                {
                    TempData["messageCreateOk"] = "Новость успешно добавлена.";
                }
                else //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    TempData["messageCreateOk"] = r.Errormessage;
                }
                return RedirectToAction("News");
            }
            return View(new_);
        }

        public ActionResult EditNew(int id)
        {
            var model = News1.Where(a => a.Id == id).FirstOrDefault();
            if (model == null)
            {
                return View("AdminErr", (object)"Новость не найдена!");
            }
            return View(model);
        }

        [HttpPost]
        //[AllowHtml]
        public async Task<ActionResult> EditNew(New new_)
        {
            if (ModelState.IsValid)
            {
                var r = await nrepo.Update<New>(new_);//repo.SaveNew(new_);
                if (r.Success)
                {
                    TempData["messageCreateOk"] = "Изменения успешно сохранены.";
                }
                else
                {
                    TempData["messageCreateOk"] = r.Errormessage;
                }
                return RedirectToAction("News");
            }
            return View(new_);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteNew(int Id)
        {
            var r = await nrepo.Delete<New>(Id);//repo.DeleteNew(Id);
            if (r.Success)
            {
                return RedirectToAction("News");
            }
            else return View("AdminErr", (object)r.Errormessage);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteService(int Id)
        {
            var r = await nrepo.Delete<Service>(Id);//repo.DeleteService(Id);
            if (r.Success)
            {
                return RedirectToAction("Services");
            }
            else return View("AdminErr", (object)r.Errormessage);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteArticle(int Id)
        {
            var r = await nrepo.Delete<Article>(Id);//repo.DeleteArticle(Id);
            if (r.Success)
            {
                return RedirectToAction("ArticlesAdmin");
            }
            else return View("AdminErr", (object)r.Errormessage);
        }

        /*-----------------------Для разного---------------------------*/

        public ActionResult Over()
        {
            var model = Over1;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Over(Over o)
        {
            if (ModelState.IsValid)
            {
                o.LM = DateTime.Now;
                var r = await nrepo.Update<Over>(o);//repo.SaveOver(o);
                if (r.Success)
                {
                    TempData["messageCreateOk"] = "Изменения успешно сохранены.";
                }
                else
                {
                    TempData["messageCreateOk"] = r.Errormessage;
                }
                //return RedirectToAction("News");
            }
            return View(o);
        }

        public ActionResult AdminErr(string s)
        {
            return View((object)s);
        }
        public ActionResult CreateSM()
        {
            string mess = "";
            try
            {
                SaveMap("SiteMap", "Map");
                mess = "sitemap.xml успешно создан";
            }
            catch (Exception exc)
            {
                mess = exc.Message;
            }
            return View((object)mess);
        }
        public ActionResult CreateCach()
        {
            string mess = "";
            try
            {
                //var path = Server.MapPath("~/") + "Cach/Home/cachIndex.html";
                SaveCach("Home", "Index", null);
                mess = "Кэш успешно создан";
            }
            catch (Exception exc)
            {
                mess = exc.Message;
            }
            return View((object)mess);
        }
        private void SaveMap(string ControllerName, string ActionName)
        {
            string path = Server.MapPath("~/") + "sitemap.xml";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var dc = this.ViewData;
            VData vd = new VData { ViewData = dc };
            var v = new RazorView(ControllerContext, "!", null, false, null);
            var tw = Response.Output;
            var h2 = new HtmlHelper(new ViewContext(ControllerContext, v, ViewData, TempData, tw), vd);
            var htmlText = h2.Action(ActionName, ControllerName).ToString() + "<!--cach-->";
            System.IO.File.WriteAllText(path, htmlText);
        }
        public void SaveCach(string ControllerName, string ActionName, object obj)
        {
            string path = Server.MapPath("~/") + "Cach/Category/Position.txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var dc = this.ViewData;
            VData vd = new VData { ViewData = dc };
            var v = new RazorView(ControllerContext, "!", null, false, null);
            var tw = Response.Output;
            var h2 = new HtmlHelper(new ViewContext(ControllerContext, v, ViewData, TempData, tw), vd);
            //h2.Action()
            var htmlText = h2.Action("Position", "Category", new { catName = "Кухни", id = 5 }).ToString() + "<!--cach-->";
            System.IO.File.WriteAllText(path, htmlText);
        }
        private string GetPath(string controllerName, string actionName, object o)
        {
            System.IO.Directory.EnumerateFiles("");
            string res = "";
            string p1 = Server.MapPath("~/") + "Cach/" + controllerName;
            string propertyValue = "";
            if (o != null)
            {
                propertyValue = o.GetType().GetProperty("id") != null ? o.GetType().GetProperty("id").GetValue(o).ToString() :
                    (o.GetType().GetProperty("categName") != null ? o.GetType().GetProperty("categName").GetValue(o).ToString() : null);
                if (propertyValue == null)
                {
                    throw new Exception("Неверные параметры для метода действий");
                }
            }
            string fn = actionName + propertyValue;
            res = p1 + "/" + fn;
            return res;
        }
        public ActionResult UpLoadImg()
        {
            var files = System.IO.Directory.EnumerateFiles(Server.MapPath("~/") + "/Content/TovarPhotos");
            foreach (var f in files)
            {
                if (f.Contains(".jpeg"))
                {
                    var f0=f.Replace(Server.MapPath("~/") + "/Content/TovarPhotos\\", "").Replace(".jpeg","");
                    var f1 = Server.MapPath("~/") + "/Content/TovarPhotos/" + f0 + "s1" + ".jpeg";
                    var f2 = Server.MapPath("~/") + "/Content/TovarPhotos/" + f0 + "s2" + ".jpeg";
                    ResizeImage(0.5, f, f1);
                    ResizeImage(0.25, f, f2);
                }
            }
            var fn = System.IO.Directory.EnumerateFiles(Server.MapPath("~/") + "/Content/TovarPhotos");
            return View(fn);
        }
        //[HttpPost]
        //public ActionResult UploadImg(HttpPostedFileBase img)
        //{
        //    var s = img.ContentType.Replace("image/", "");
        //    var path =  "/Content/TovarPhotos/" + "test" + "." + s;
        //    var p2=Server.MapPath("~/") +"/Content/TovarPhotos/" + "small" + "." + s;
        //    var p3 = Server.MapPath("~/") + "/Content/TovarPhotos/" + "small2" + "." + s;
        //    var fp=Server.MapPath("~/") + path;
        //    img.SaveAs(fp);
        //    ResizeImage(0.5,fp, p2);
        //    ResizeImage(0.25, fp, p3);
        //    return RedirectToAction("TestView", new { p = path, pp = "/Content/TovarPhotos/" + "small" + "." + s, ppp = "/Content/TovarPhotos/" + "small2" + "." + s });
        //}
        //public ActionResult TestView(string p,string pp,string ppp)
        //{
        //    string[] model = new string[3];
        //    model[0] = p;
        //    model[1] = pp;
        //    model[2] = ppp;
        //    return View(model);
        //}

        private void ResizeImage(double scale, string filePath, string saveFilePath)
        {
            //variables for image dimension/scale
            double newHeight = 0;
            double newWidth = 0;
           // double scale = 0.5;
            //create new image object
            Bitmap curImage = new Bitmap(filePath);
            //Determine image scaling
            //if (curImage.Height > curImage.Width)
            //{
            //    scale = Convert.ToSingle(size) / curImage.Height;
            //}
            //else
            //{
            //    scale = Convert.ToSingle(size) / curImage.Width;
            //}
            //New image dimension
            newHeight = Math.Floor(Convert.ToSingle(curImage.Height) * scale);
            newWidth = Math.Floor(Convert.ToSingle(curImage.Width) * scale);
            //Create new object image
            Bitmap newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
            Graphics imgDest = Graphics.FromImage(newImage);
            imgDest.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            imgDest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            imgDest.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            imgDest.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters param = new EncoderParameters(1);
            param.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            //Draw the object image
            imgDest.DrawImage(curImage, 0, 0, newImage.Width, newImage.Height);
            //Save image file
            newImage.Save(saveFilePath, info[1], param);
            //Dispose the image objects
            curImage.Dispose();
            newImage.Dispose();
            imgDest.Dispose();
        }

    }
}