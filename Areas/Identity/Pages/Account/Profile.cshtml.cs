using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment_1_G_92_2139.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment_1_G_92_2139.Areas.Identity.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "Contact Number")]
            public string ContactNumber { get; set; }

            [Display(Name = "Preferred Categories")]
            public string PreferredCategories { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            Input = new InputModel
            {
                FullName = user.FullName,
                ContactNumber = user.ContactNumber,
                PreferredCategories = user.PreferredCategories
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found.");

            user.FullName = Input.FullName;
            user.ContactNumber = Input.ContactNumber;
            user.PreferredCategories = Input.PreferredCategories;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                StatusMessage = "Profile updated successfully.";
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}
