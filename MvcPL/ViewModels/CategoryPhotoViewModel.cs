using System.ComponentModel.DataAnnotations;

namespace MvcPL.ViewModels
{
    public class CategoryPhotoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Category photo name")]
        [System.Web.Mvc.Remote("ValidateCategoryName", "Category", ErrorMessage = "Category exist with this name.")]
        public string Name { get; set; }
    }
}