//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Talisman.Domain.Concrete;
//using Talisman.Domain.Entities;
//using Talisman.Domain.Abstract;
//using System.IO;

//namespace Talisman.WebUI.Controllers
//{
//    public class LayoutController : Controller
//    {
//        private IRepository repo;
//        public LayoutController(IRepository r)
//        {
//            repo = r;
//        }
//        public FileContentResult GetImage(int photoId) 
//        {

//            //var Photos = (HttpContext.Application["GlobalData"] as GlobalData).Images;
//            Photo photo = repo.Photos.FirstOrDefault(p => p.PhotoId == photoId); //repository.Products
//            //.FirstOrDefault(p => p.ProductID == productId);
//            if (photo != null)
//            {
//                var f = File(photo.Image, photo.ImageMimeType);
//                //var f2=new System.IO.File()
//                System.IO.File.WriteAllBytes("p", photo.Image);
//                return File(photo.Image, photo.ImageMimeType);
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}