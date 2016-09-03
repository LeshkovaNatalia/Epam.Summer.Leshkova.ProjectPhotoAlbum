using System.ComponentModel.DataAnnotations;

namespace MvcPL.ViewModels
{
    public class CategoryPhotoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Category photo name")]
        public string Name { get; set; }
    }
}