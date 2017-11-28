using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Talisman.Domain.Concrete;
using Talisman.Domain.Entities;

namespace Talisman.WebUI.App_Start
{
    //public class DataReader
    //{
    //    public static void Init(HttpContextBase context)
    //    {
    //        var repo = new Repository();
    //        var p = repo.Photos.Select(ph => new Photo { PhotoId = ph.PhotoId, TovarId = ph.TovarId }).ToList();
    //        var Articles = new List<Article>(repo.Articles);
    //        var Categories = new List<Categorie>(repo.Categories);
    //        var Images = repo.Photos;
    //        var Photos = new List<Photo>(p);
    //        var Tovars = new List<Tovar>(repo.Tovars);

    //        // this.Context.Application["GlobalData"] = g;
    //        context.Application["Articles"] = Articles;
    //        context.Application["Categories"] = Categories;
    //        context.Application["Images"] = Images;
    //        context.Application["Photos"] = Photos;
    //        context.Application["Tovars"] = Tovars;
    //    }
    //}
}