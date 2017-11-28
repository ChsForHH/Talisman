using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talisman.Domain.Concrete;
using System.Threading.Tasks;
using Talisman.WebUI.Controllers;
using System.IO;
using Talisman.Domain.Entities;
using Talisman.WebUI.Infrastructure;

namespace Talisman.WebUI
{
    public class ChangeCachEventArgs : EventArgs
    {
        public ActionType actionType;
        public Type type;
        public object obj;
        public ChangeCachEventArgs(ActionType at, Type t, object o)
        {
            actionType = at;
            type = t;
            obj = o;
        }
    }
    public delegate void ChangeCachEventHahdler(object source, ChangeCachEventArgs arg);

    public class CachTools
    {
        private CachController controller;
        private GData3 g;
        public CachTools()
        {
            controller = new CachController();
            g = new GData3();
            ChangeCach += this.Handler;
        }
        public event ChangeCachEventHahdler ChangeCach;
        public void OnChanchCach(ActionType at, Type t, object o)
        {
            var arg = new ChangeCachEventArgs(at, t, o);
            if (ChangeCach != null)
            {
                ChangeCach(this, arg);
            }
        }
        private void Handler(object o, ChangeCachEventArgs arg)
        {
            
            switch (arg.type.Name)
            {                
                case ("Categorie"):
                    SaveIndex();
                    Categorie category = arg.obj as Categorie;
                    if (arg.actionType == ActionType.Delete)
                    {
                        try
                        {
                            File.Delete(HttpContext.Current.Server.MapPath("~/") + "Cach/" + category.CategoryName.Replace(" ", "-"));
                        }
                        catch { }
                    }
                    else
                    {                        
                        SaveCach("Category", "Category", new { categName = category.CategoryName.Replace(" ", "-") });
                    }
                    break;                                
                case("Service"):
                    break;
                default:
                    SaveIndex();
                    break;
            }
                              
        }
        private void SaveCach(string controllerName, string actionName, object o)
        {
            //var controller = new CachController();
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(HttpContext.Current.Request.RequestContext, controller);            
            controller.SaveCach(controllerName, actionName, o);            
        }
        private void SaveIndex()
        {
            SaveCach("Home", "Index", null);
        }
    }
    public enum ActionType { Insert, Update, Delete };

    public class NewRepository
    {
        private ApiDb api;
        public NewRepository()
        {
            api = new ApiDb();
        }

        private void SetCacheNull<T>()
        {
            var g = System.Web.HttpContext.Current.Application["GlobalData"] as GlobalData;
            string t = typeof(T).Name;
            switch (t)
            {
                case "Tovar": g.Tovars = null; g.TaP = null; break;
                case "Categorie": g.Categories = null; g.TaP = null; break;
                case "Article": g.Articles = null; break;
                case "Photo2": g.Photos = null; g.TaP = null; break;
                case "Photo": g.Photos = null; g.TaP = null; break;
                case "Service": g.Services = null; break;
                case "FeedBack": g.FeedBacks = null; break;
                case "Over": g.Over = null; break;
                case "New": g.News = null; break;
            }
        }

        public async Task<Result> Insert<T>(T o) where T : new()
        {
            Result r = null;            
            try
            {
                r = await api.Ins<T>(o);
                SetCacheNull<T>();
                if (HttpContext.Current.Application["CachTools"] == null)
                {
                    HttpContext.Current.Application.Add("CachTools", new CachTools());
                }
                (HttpContext.Current.Application["CachTools"] as CachTools).OnChanchCach(ActionType.Insert, typeof(T), o);
            }
            catch (Exception exc)
            {
                r = new Result(false, exc.Message);
            }
            return r;
        }
        public async Task<Result> Update<T>(T o) where T : new()
        {
            Result r = null;
            try
            {
                r = await api.Upd<T>(o);
                SetCacheNull<T>();
                if (HttpContext.Current.Application["CachTools"] == null)
                {
                    HttpContext.Current.Application.Add("CachTools", new CachTools());
                }
                (HttpContext.Current.Application["CachTools"] as CachTools).OnChanchCach(ActionType.Update, typeof(T), o);
            }
            catch (Exception exc)
            {
                r = new Result(false, exc.Message);
            }
            return r;
        }
        public async Task<Result> Delete<T>(int index) where T : new()
        {
            Result r = null;
            try
            {
                r = await api.Del<T>(index);
                SetCacheNull<T>();
                if (HttpContext.Current.Application["CachTools"] == null)
                {
                    HttpContext.Current.Application.Add("CachTools", new CachTools());
                }
                (HttpContext.Current.Application["CachTools"] as CachTools).OnChanchCach(ActionType.Delete, typeof(T), (object)index);
            }
            catch (Exception exc)
            {
                r = new Result(false, exc.Message);
            }
            return r;
        }
    }
}