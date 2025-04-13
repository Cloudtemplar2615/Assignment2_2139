using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Assignment_1_G_92_2139.Models;

namespace Assignment_1_G_92_2139.Data
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create or update default admin user
            var adminEmail = "admin@site.com";
            var adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "Admin User",
                    ContactNumber = "1234567890",
                    PreferredCategories = "All"
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            else
            {
                
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(adminUser);
                var resetResult = await userManager.ResetPasswordAsync(adminUser, resetToken, adminPassword);

                if (resetResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin"); // In case role is missing
                }
            }
        }
    }
}
