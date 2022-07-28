using System.ComponentModel.DataAnnotations;

namespace productMgtApi.Controllers.Resources
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email {get; set;}

        [Required(ErrorMessage = "Password is required")]
        public string? Password {get; set;}

        [Required(ErrorMessage = "Username is required")]
        public string? UserName {get; set;}

    }
}