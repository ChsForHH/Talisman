using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talisman.WebUI.Infrastructure.Concreet;
using Talisman.WebUI.Infrastructure.Abstract;
using Talisman.WebUI.Models;
using System.Web.Security;
using Talisman.Domain.Concrete;
using System.Threading.Tasks;

namespace Talisman.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController()
        {
            authProvider = new FormsAuthProvider();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                var api = new ApiDb2();
                var s = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password + "chs", "md5");
                var r = await api.Get(model.UserName, s);
                if (r.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    //FormsAuthentication.RedirectFromLoginPage()
                    return RedirectToAction("ListCategories","Admin");//(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Change()
        {
            return View();
        }
        [HttpPost]
        public async Task< ActionResult> Change(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = new ApiDb2();
                var s = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password+"chs", "md5");
               // var s = model.Password;
                var r = await api.Ins(model.UserName, s);
                if (r.Success)
                {
                    ViewBag.resultOk = "Изменения успешно сохранены!";
                    return View();
                }
                else
                {
                    ViewBag.resultFail = "Ошибка! " + r.Errormessage;
                }

            }
            else
            {
                return View();
            }
            return View();
        }
        public ActionResult SignOut()
        {
            authProvider.SignOut();
            return RedirectToAction("Index", "Home");
        }        
    }
}