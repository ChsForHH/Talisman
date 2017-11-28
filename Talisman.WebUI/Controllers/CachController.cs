using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.IO;

namespace Talisman.WebUI.Controllers
{
    public class CachController : Controller
    {
        public void SaveCach(string ControllerName, string ActionName, object obj)
        {
            string path = GetPath(ControllerName, ActionName, obj);            
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var dc = this.ViewData;
            VData vd = new VData { ViewData = dc };
            var v = new RazorView(ControllerContext, "!", null, false, null);
            var tw = Response.Output;
            var h2 = new HtmlHelper(new ViewContext(ControllerContext, v, ViewData, TempData, tw), vd);
            var htmlText = "просто текст";
            //Устанавливаем флаг чтобы запретить отправку http-заголовков!
            HttpContext.Application["IsLocked"] = true;
            try
            {
                htmlText = (h2.Action(ActionName, ControllerName, obj)).ToString()+"<!--cach-->";
                System.IO.File.WriteAllText(path, htmlText);
            }
            catch (Exception exc)
            {
                try
                {
                    System.IO.File.AppendAllText(Server.MapPath("~/") + "ErrorsLog/Log.txt",exc.Message+"\n\t");
                }
                catch { }
            }
            finally
            {
                HttpContext.Application["IsLocked"] = false;
            }
            
        }
        private string GetPath(string controllerName, string actionName, object o)
        {
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
            //System.IO.File.WriteAllText(Server.MapPath("~/") + "Cach/log.txt", res);
            //System.IO.File.WriteAllText(Server.MapPath("~/") + "Cach/log.txt", "cn=" + o.GetType().GetProperty("catName").GetValue(o) + " id=" + o.GetType().GetProperty("id").GetValue(o));
            return res;
        }
    }
}