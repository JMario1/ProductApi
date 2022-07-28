

namespace productMgtApi.Domain.Models
{
    public class Product
    {
        public int Id {get; set;}
        public string? Name {get; set;}
        public int Price {get; set;}
        public int AvaliableStock {get; set;}
        public string? Description {get; set;}
        public bool Disabled {get; set;}
        public DateTime CreatedAt {get; set;}
        public int CategoryId {get; set;}
        public Category? Category {get; set;}

    }
}