using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Talisman.WebUI.Models
{
    public class EditCategory
    {
        
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название категории.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage="Пожалуйста, введите статью описания категории.")]
        public string Article { get; set; }

        public string Rem { get; set; }
    }
}