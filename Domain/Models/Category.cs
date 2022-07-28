using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace productMgtApi.Domain.Models
{
    public class Category
    {
        public int Id;
        public string? Name { get; set; }
        public string? Description {get; set;}
    }
}