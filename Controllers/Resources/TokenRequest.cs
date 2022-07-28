using System.ComponentModel.DataAnnotations;

namespace productMgtApi.Controllers.Resources
{
    public class TokenRequest
    {
        [Required]
        public string? Token { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string? Email { get; set; }
    }
}