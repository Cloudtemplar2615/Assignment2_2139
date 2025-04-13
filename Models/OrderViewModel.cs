using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment_1_G_92_2139.Models
{
    public class OrderViewModel
    {
        [Required]
        public string GuestName { get; set; }

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }

    public class OrderItemViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}