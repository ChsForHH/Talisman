using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Talisman.Domain.Entities
{
    public class Tovar
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int TovarId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }


        public string Name { get; set; }
        [Required]
        [AllowHtml]
        public string Describing { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public bool Visible { get; set; }
        public bool IsNew { get; set; }
        [Required]
        public string CEO { get; set; }

        public DateTime LM { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string KeyWords { get; set; }
    }

    public class Photo2
    {
        [Key]
        public int PhotoId { get; set; }
        public int TovarId { get; set; }
        public byte[] Image { get; set; }
        public byte[] Mini { get; set; }
        public string ImageMimeType { get; set; }
        public DateTime LM { get; set; }
    }
    public class Photo   
    {
        [Key]
        public int PhotoId { get; set; }
        public int TovarId { get; set; }
        //public byte[] Image { get; set; }
        //public byte[] Mini { get; set; }
        //public string ImageMimeType { get; set; }
    }

    public class Categorie
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [AllowHtml]
        public string Article { get; set; }
        public bool Visible { get; set; }
        [Required]
        public string CEO { get; set; }
        public DateTime LM { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string KeyWords { get; set; }
    }

    public class Article
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage="Пожалуйста, введите название статьи")]
        public string Name { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Пожалуйста, введите содержимое (контент) статьи")]
        public string Content { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Author { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime Date { get; set; }
        public bool Visible { get; set; }
        [Required]
        public string CEO { get; set; }
    }

    public class ArtMin
    {
        [Key]        
        public int Id { get; set; }        
        public string Name { get; set; }
    }

    public class FeedBack
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите текст отзыва")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Message { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите свой адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }

        public string Contacts { get; set; }

        public bool Visible { get; set; }
    }

    public class Service
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите наименование услуги")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите текст статьи описания услуги")]
        [AllowHtml]
        public string Article { get; set; }

        public bool Visible { get; set; }
        public DateTime LM { get; set; }
    }
    public class New
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage="Введите дату")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Введите содержимое новости")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Введите url ссылки на новость")]
        public string Url { get; set; }

        public bool Visible { get; set; }
    }

    public class Over
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Bussines { get; set; }
        [AllowHtml]
        [Required]
        public string Address { get;set; }
        [Required]
        [AllowHtml]
        public string Address2 { get; set; } 
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email{get;set;}
        [Required]
        public string Regim { get; set; }
        [AllowHtml]
        [Required]
        public string ArticleMain { get; set; }
        [AllowHtml]
        [Required]
        public string Article2 { get; set; }
        [AllowHtml]
        [Required]
        public string ArticleFoot { get; set; }
        [Required]
        public string Link1 { get; set; }
        [Required]
        public string Link2 { get; set; }
        [Required]
        public string Link3 { get; set; }
        [Required]
        public string CEO { get; set; }
        public DateTime LM { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string KeyWords { get; set; }
    }

    public class TovarsAndPhoto
    {
        public int TovarId { get; set; }
        public int CategoryId { get; set; } 
        public string Name { get; set; }
        public double? Price { get; set; }
        public bool IsNew { get; set; }
        public int PhotoId { get; set; }
        public DateTime LM { get; set; }
    }

    public class TovarViewModel
    {
        public Tovar Tovar { get; set; }
        public int PhotoId { get; set; }
    }
}
