using System.ComponentModel.DataAnnotations;

namespace Assignment_1_G_92_2139.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        

        public ICollection<Product> Products { get; set; }
    }
}