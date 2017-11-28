using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using Talisman.Domain.Concrete;
namespace Talisman.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Def",
                url: "def",
                defaults: new { controller = "Def" }
                );
            routes.MapRoute(
                name: "RoteServ",
                url: "Услуги",
                defaults: new { controller = "Services", action = "List" }
                );
            routes.MapRoute(
                name: "RoteFB",
                url: "Отзывы",
                defaults: new { controller = "FeedBack", action = "List" }
                );
            routes.MapRoute(
                name: "RoteCB",
                url: "CB",
                defaults: new { controller = "Home", action = "CallBack" }
                );
                routes.MapRoute(
                 name: "RoteCont",
                 url: "Контакты",
                 defaults: new { controller = "Home", action = "Contacts" }
                 );
                routes.MapRoute(
                           name: "RoteArtf",
                           url: "Статьи",
                           defaults: new { controller = "Articles", action = "Article", artName="" }
                           );
                routes.MapRoute(
                        name: "HomeErr",
                        url: "Home/{action}",
                        defaults: new { controller = "Error", action = "NotFound" }
                        );
                routes.MapRoute(
                            name: "CategoryErr",
                            url: "category/{action}",
                            defaults: new { controller = "Error", action = "NotFound" }
                            );
                routes.MapRoute(
                                name: "servicesErr",
                                url: "services/{action}",
                                defaults: new { controller = "Error", action = "NotFound" }
                                );
                routes.MapRoute(
                                    name: "articlesErr",
                                    url: "articles/{action}",
                                    defaults: new { controller = "Error", action = "NotFound" }
                                    );
                routes.MapRoute(
                                        name: "feedbackErr",
                                        url: "feedback/{action}",
                                        defaults: new { controller = "Error", action = "NotFound" }
                                        );
                routes.MapRoute(
                                            name: "errErr",
                                            url: "error/{action}",
                                            defaults: new { controller = "Error", action = "NotFound" }
                                            );
                routes.MapRoute(
                       name: "RoteArt",
                       url: "Статьи/{artName}",
                       defaults: new { controller = "Articles", action = "Article" }
                       );
                routes.MapRoute(
                name: "RoteCat",
                url: "{categName}",
                defaults: new { controller = "Category", action = "Category" }
                );
            
                   
                
            
            //routes.MapRoute(
            //    name: "RoteHomeView",
            //    url: "HomeViewer/{tovarId}",
            //    defaults: new { controller = "Home", action = "Viewer" }
            //);
            //routes.MapRoute(
            //    name: "RoteCatView",
            //    url: "Viewer/{tovarId}",
            //    defaults: new { controller = "Category", action = "Viewer" }
            //);
            routes.MapRoute(
                 "Admin",
                "Admin/{action}/{id}",
                 new { controller = "Admin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 "Position",
                "{catName}/модель-{id}",
                 new { controller = "Category", action = "Position" }
            );
            routes.MapRoute(
                 "PositionNo",
                "category/position/{id}",
                 new { controller = "undef", action = "undef" }
            );
            
            routes.MapRoute(
                 "Default",
                "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
