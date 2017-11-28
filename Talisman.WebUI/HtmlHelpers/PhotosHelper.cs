using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Talisman.Domain.Concrete;

namespace Talisman.WebUI.HtmlHelpers
{
    public static class PhotosHelper
    {
        
        public static MvcHtmlString PSrc(this HtmlHelper html, int id,int size)
        {
            string rez = "";
            switch (size)
            {
                case 1: rez="/Content/TovarPhotos/" + id.ToString() + ".jpeg"; break;
                case 2: rez = "/Content/TovarPhotos/" + id.ToString()+"s1" + ".jpeg"; break;
                case 3: rez = "/Content/TovarPhotos/" + id.ToString()+"s2" + ".jpeg"; break;

            }
            return new MvcHtmlString(rez);
        }
        //{
    }

    public static class Over
    {
        private static GData3 g = new GData3();
        public static MvcHtmlString PrevLink(this HtmlHelper html, int id)
        {

            var Url = new System.Web.Mvc.UrlHelper(html.ViewContext.HttpContext.Request.RequestContext);
            var tov = g.Tovars.Where(t => t.TovarId == id).FirstOrDefault();
            var cName = g.Categories.Where(c => c.CategoryId == tov.CategoryId).FirstOrDefault().CategoryName.Replace(" ", "-");
            var prevTovar = g.Tovars.Where(t => t.TovarId < id && t.CategoryId == tov.CategoryId && t.Visible);
            if (prevTovar.Count() == 0)
            {
                return null;
            }
            else
            {
                int prevId = prevTovar.Max(t => t.TovarId);                
                return new MvcHtmlString(Url.Action("Position", "Category", new { catName = cName, id = prevId }));                
            }
        }
        public static MvcHtmlString NextLink(this HtmlHelper html, int id)
        {

            var Url = new System.Web.Mvc.UrlHelper(html.ViewContext.HttpContext.Request.RequestContext);
            var tov = g.Tovars.Where(t => t.TovarId == id).FirstOrDefault();
            var cName = g.Categories.Where(c => c.CategoryId == tov.CategoryId).FirstOrDefault().CategoryName.Replace(" ", "-");
            var nextTovar = g.Tovars.Where(t => t.TovarId > id && t.CategoryId == tov.CategoryId && t.Visible);
            if (nextTovar.Count() == 0)
            {
                return null;
            }
            else
            {
                int prevId = nextTovar.Min(t => t.TovarId);                
                return new MvcHtmlString(Url.Action("Position", "Category", new { catName = cName, id = prevId }));                
            }
        }
        public static MvcHtmlString PrevLink(this HtmlHelper html, int id,string className)
        {
            
            var Url=new System.Web.Mvc.UrlHelper(html.ViewContext.HttpContext.Request.RequestContext); 
            var tov=g.Tovars.Where(t=>t.TovarId==id).FirstOrDefault();
            var cName = g.Categories.Where(c => c.CategoryId == tov.CategoryId).FirstOrDefault().CategoryName.Replace(" ", "-");
            var prevTovar = g.Tovars.Where(t => t.TovarId < id && t.CategoryId == tov.CategoryId && t.Visible);
            if (prevTovar.Count() == 0)
            {
                return null;
            }
            else
            {
                int prevId = prevTovar.Max(t => t.TovarId);
                var link = new TagBuilder("a");
                link.MergeAttribute("href", Url.Action("Position", "Category", new { catName = cName, id = prevId }));
                link.AddCssClass(className);
                link.MergeAttribute("title", "Предыдущая модель");
                link.InnerHtml = "<span class='glyphicon glyphicon-arrow-left'></span>";
                return new MvcHtmlString(link.ToString());
            }
        }
        public static MvcHtmlString NextLink(this HtmlHelper html, int id, string className) 
        {

            var Url = new System.Web.Mvc.UrlHelper(html.ViewContext.HttpContext.Request.RequestContext);
            var tov = g.Tovars.Where(t => t.TovarId == id).FirstOrDefault();
            var cName = g.Categories.Where(c => c.CategoryId == tov.CategoryId).FirstOrDefault().CategoryName.Replace(" ", "-");
            var nextTovar = g.Tovars.Where(t => t.TovarId > id && t.CategoryId == tov.CategoryId && t.Visible);
            if (nextTovar.Count() == 0)
            {
                return null;
            }
            else
            {
                int prevId = nextTovar.Min(t => t.TovarId);
                var link = new TagBuilder("a");
                link.MergeAttribute("href", Url.Action("Position", "Category", new { catName = cName, id = prevId }));
                link.MergeAttribute("title", "Следующая модель");
                link.AddCssClass(className);
                link.InnerHtml = "<span class='glyphicon glyphicon-arrow-right'></span>";
                return new MvcHtmlString(link.ToString());
            }
        }

        public static MvcHtmlString CompanyName(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.CompanyName);
        }

        public static MvcHtmlString Title(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Title);
        }

        public static MvcHtmlString KeyWords(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.KeyWords);
        }

        public static MvcHtmlString Bussines(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Bussines);
        }

        public static MvcHtmlString Phone(this HtmlHelper html) 
        {
            return new MvcHtmlString(g.Over.Phone);
        }

        public static MvcHtmlString Email(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Email);
        }

        public static MvcHtmlString Address(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Address);
        }

        public static MvcHtmlString Address2(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Address2); 
        }

        public static MvcHtmlString Regim(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Regim);
        }

        public static MvcHtmlString VKLink(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Link1);
        }

        public static MvcHtmlString FBLink(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Link2); 
        }

        public static MvcHtmlString InstLink(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Link3);
        }

        public static MvcHtmlString MainArticle(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.ArticleMain);
        }

        public static MvcHtmlString Article2(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.Article2);
        }

        public static MvcHtmlString ArticleFoot(this HtmlHelper html) 
        {
            return new MvcHtmlString(g.Over.ArticleFoot);
        }
        public static MvcHtmlString CeoMain(this HtmlHelper html)
        {
            return new MvcHtmlString(g.Over.CEO);
        }
    }
    public class Numer
    {
        private Dictionary<string, string> numwords1;
        private Dictionary<string, string> numwords2;
        private Dictionary<string, string> numwords3;
        private Dictionary<string, string> numwords4;       
        public Numer()
        {
            numwords1 = new Dictionary<string, string>();
            numwords1.Add("0", "");
            numwords1.Add("1", "один");
            numwords1.Add("2", "два");
            numwords1.Add("3", "три");
            numwords1.Add("4", "четыре");
            numwords1.Add("5", "пять");
            numwords1.Add("6", "шесть");
            numwords1.Add("7", "семь");
            numwords1.Add("8", "восемь");
            numwords1.Add("9", "девять");
            numwords1.Add("10", "десять");
            numwords1.Add("11", "одиннадцать");
            numwords1.Add("12", "двенадцать");
            numwords1.Add("13", "тринадцать");
            numwords1.Add("14", "четырнадцать");
            numwords1.Add("15", "пятнадцать");
            numwords1.Add("16", "шестнадцать");
            numwords1.Add("17", "семнадцать");
            numwords1.Add("18", "восемнадцать");
            numwords1.Add("19", "девятнадцать");

            numwords2 = new Dictionary<string, string>();
            numwords2.Add("0", "");
            numwords2.Add("2", "двадцать");
            numwords2.Add("3", "тридцать");
            numwords2.Add("4", "сорок");
            numwords2.Add("5", "пятьдесят");
            numwords2.Add("6", "шестьдесят");
            numwords2.Add("7", "семьдесят");
            numwords2.Add("8", "восемьдесят");
            numwords2.Add("9", "девяносто");

            numwords3 = new Dictionary<string, string>();
            numwords3.Add("0", "");
            numwords3.Add("1", "сто");
            numwords3.Add("2", "двести");
            numwords3.Add("3", "триста");
            numwords3.Add("4", "четыреста");
            numwords3.Add("5", "пятьсот");
            numwords3.Add("6", "шестьсот");
            numwords3.Add("7", "семьсот");
            numwords3.Add("8", "восемьсот");
            numwords3.Add("9", "девятьсот");

            numwords4 = new Dictionary<string, string>();
            numwords4.Add("0", "");
            numwords4.Add("1", "тысяча");
            numwords4.Add("2", "две тысячи");
            numwords4.Add("3", "три тысячи");
            numwords4.Add("4", "четыре тысячи");
            numwords4.Add("5", "пять тысяч");
            numwords4.Add("6", "шесть тысяч");
            numwords4.Add("7", "семь тысяч");
            numwords4.Add("8", "восемь тысяч");
            numwords4.Add("9", "девять тысяч");
        }
        private string GetDoubleNum(string num){
            string rez="";
            if (num[0] == '1')
                    {
                        rez = numwords1[num];
                    }
                    else
                    {
                        //var ch = num[1]; var ch2=new string()
                        rez = numwords2[num[0].ToString()] + " " + numwords1[num[1].ToString()];
                    }
            return rez;
        }
        public string getNumWord(string num){
            int n=0;
            Int32.TryParse(num, out n);
            string rez = "";            
            if(num.Length>4 || num==null || num==string.Empty || n==0){
                rez= "безымянная";
            }
            else
            {
                string t = "";
                switch (num.Length)
                {
                    case 1: rez = numwords1[num];
                        break;
                    case 2: rez = GetDoubleNum(num);
                        break;
                    case 3: t = num.Substring(1); rez = numwords3[num[0].ToString()] + " " + GetDoubleNum(t);
                        break;
                    default: t = num.Substring(2); rez = numwords4[num[0].ToString()] + " " + numwords3[num[0].ToString()] + " " + GetDoubleNum(t);
                        break;
                }                
            }
            return rez;
        }
        
    }

    public static class NumerHelper
    {
        public static MvcHtmlString GetNumWord(this HtmlHelper html,string num)
        {
            var nw = new Numer().getNumWord(num);
            return new MvcHtmlString(nw);
        }
    }
}