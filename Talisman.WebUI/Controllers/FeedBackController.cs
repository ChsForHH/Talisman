using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using System.Threading.Tasks;
using Talisman.Domain.Concrete;
namespace Talisman.WebUI.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class FeedBackController : Controller
    {
        private GData3 g;
        //IRepository repo;
        public FeedBackController()
        {
            g = new GData3();
            //this.g = g;
        }
        // GET: FeedBack
        public ActionResult List() 
        {
            var model = g.FeedBacks.Where(f=>f.Visible);
            Response.Cache.SetLastModified(g.Get_LM(HeadersFor.Feedbacks));
            return View(model);
        }

        public PartialViewResult AddFeedBack()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<PartialViewResult> AddFeedBack(FeedBack fb)
        {
            if (ModelState.IsValid)
            {
                fb.Id = 0;
                fb.Date = DateTime.Now;
                if (fb.Contacts == null)
                {
                    fb.Contacts = "";
                }
                //var res = await repo.SaveFeedBack(fb);
                var repo = new NewRepository();
                var res = await repo.Insert<FeedBack>(fb);
                if (res.Success)
                {
                    ViewBag.cl = "fbOk";
                    ViewBag.mess = "Спасибо! Ваш отзыв успешно отправлен на модерацию.";
                    return PartialView("FBres");
                }
                else
                {
                    ViewBag.cl = "fbErr";
                    ViewBag.mess = res.Errormessage;
                }
                return PartialView("FBres");
            }
            return PartialView(fb);
        }
    }
}