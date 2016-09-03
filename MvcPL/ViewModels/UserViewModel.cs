using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "User's e-mail")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Date of user's registration")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "User's photo")]
        public byte[] Photo { get; set; }

        [Display(Name = "User's role in the system")]
        public string[] Roles { get; set; }
    }
}