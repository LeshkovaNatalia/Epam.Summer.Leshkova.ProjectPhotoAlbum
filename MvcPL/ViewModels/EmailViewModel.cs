using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class EmailViewModel
    {
        [Display(Name = "Enter your e-mail")]
        [Required(ErrorMessage = "The field Email can not be empty!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email.")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "The message must contain at least {2} characters.", MinimumLength = 50)]
        [Required(ErrorMessage = "The field Message can not be empty!")]
        public string Message { get; set; }
    }
}