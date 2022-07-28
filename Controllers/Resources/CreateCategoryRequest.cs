using System.ComponentModel.DataAnnotations;

namespace productMgtApi.Controllers.Resources
{
    public class CreateCategoryRequest
    {
        
        [Required(ErrorMessage = "Name is required")]
        public string? Name {get; set;}

        [Required(ErrorMessage = "Category description is required")]
        public string? Description {get; set;}
    }
}