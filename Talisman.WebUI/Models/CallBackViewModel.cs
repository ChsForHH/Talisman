using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Talisman.WebUI.Models
{
    public class CallBackViewModel
    {
        [Required(ErrorMessage = "Введите имя.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите номер.")]
        public string Number { get; set; }
    }
}