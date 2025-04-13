using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Assignment_1_G_92_2139.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        // Optional
        public string PreferredCategories { get; set; }
    }
}
