using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using Talisman.Domain.Concrete;
using System.Threading.Tasks;
using Talisman.WebUI.Controllers;
//using System.Web.Optimization;

namespace Talisman.WebUI
{
   

    public class MvcApplication : System.Web.HttpApplication
    {                
        public MvcApplication()
        {
            
        }
        
        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            this.Context.Application.Add("GlobalData", new GlobalData());
            this.Context.Application.Add("IsLocked", false);
            //Init2().Wait();
            
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //private async Task Init2()
        //{
        //    var repo = new ApiDb();
        //    var g = new GlobalData
        //    {
        //        ArtMins = await repo.Get<ArtMin>(),
        //        News = await repo.Get<New>(),
        //        Over = (await repo.Get<Over>()).FirstOrDefault(),
        //        Categories = await repo.Get<Categorie>(),
        //        TaP = await repo.Get<TovarsAndPhoto>()
        //    };
        //    //var CT = new CachTools();
        //    //this.Context.Application.Add("CachTools", CT);      
        //    this.Context.Application["GlobalData"] = g;
        //}

protected void Application_Error(object sender, EventArgs e)
{
    HttpContext ctx = HttpContext.Current;
    Exception ex = ctx.Server.GetLastError();
    ctx.Response.Clear();
    ctx.Response.Write("Перехват!");
    RequestContext rc = ((MvcHandler)ctx.CurrentHandler).RequestContext;
    IController controller = new ErrorController(); 
    var context = new ControllerContext(rc, (ControllerBase)controller);
    var viewResult = new ViewResult();
    var httpException = ex as HttpException;        
    if (httpException != null)
    {
        switch (httpException.GetHttpCode())
        {
            case 404:
                viewResult.ViewName = "NotFound";
                rc.HttpContext.Response.StatusCode = 404;
                break;
            case 403:
                viewResult.ViewName = "Forbidden";
                rc.HttpContext.Response.StatusCode = 403;
                break;
            case 500:
                viewResult.ViewName = "InternalServerError";
                rc.HttpContext.Response.StatusCode = 500;
                break;
            default:                
                viewResult.ViewName = "Error";
                rc.HttpContext.Response.StatusCode = 500;
                break;
        }
    }
    else
    {
        viewResult.ViewName = "Error";
    }
    viewResult.ViewData.Model = new HandleErrorInfo(ex, context.RouteData.GetRequiredString("controller"), context.RouteData.GetRequiredString("action"));
    viewResult.ExecuteResult(context);
    ctx.Server.ClearError();
}
    }
}
