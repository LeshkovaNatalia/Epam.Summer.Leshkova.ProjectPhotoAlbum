using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcPL.ViewModels
{
    public class PhotoViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
    }
}