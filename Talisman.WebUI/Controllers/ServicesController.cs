using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using Talisman.Domain.Concrete;

namespace Talisman.WebUI.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class ServicesController : Controller
    {
        private GData3 g;
        public ServicesController()
        {
            this.g = new GData3();
        }
        //[OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
        public ActionResult List() 
        {
            var model = g.Services.Where(s => s.Visible);
            Response.Cache.SetLastModified(g.Get_LM(HeadersFor.Services));
            return View(model);
        }
    }
}