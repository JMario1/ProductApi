using System.ComponentModel.DataAnnotations;

namespace productMgtApi.Controllers.Resources
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name {get; set;}

        [Required(ErrorMessage = "Price is required")]
        public int Price {get; set;}

        [Required(ErrorMessage = "Avaliable stock is required")]
        public int AvaliableStock {get; set;}

        [Required(ErrorMessage = "Category id is required")]
        public int CategoryId {get; set;}

        [Required(ErrorMessage = "Product description is required")]
        public string? Description {get; set;}
    }
}