using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole> {
                new AppRole { Id = Guid.NewGuid(), Name = "Admin" },
                new AppRole { Id = Guid.NewGuid(), Name = "Owner" },
                new AppRole { Id = Guid.NewGuid(), Name = "Guest" },
                new AppRole { Id = Guid.NewGuid(), Name = "Host" },
                new AppRole { Id = Guid.NewGuid(), Name = "Member" }
            };

            if (!await roleManager.Roles.AnyAsync())
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            /*var users = new List<AppUser> {
                new AppUser { UserName = "hoainam10th", DisplayName = "Hoài Nam" },
                new AppUser{ UserName="ubuntu", DisplayName = "Ubuntu Nguyễn" },
                new AppUser{UserName="lisa", DisplayName = "Lisa" }
            };

            foreach (var user in users)
            {
                //user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "admin@123");
                await userManager.AddToRoleAsync(user, "Guest");
            }*/

            var admin = new AppUser { UserName = "admin", DisplayName = "Administrator" };
            await userManager.CreateAsync(admin, "admin@123");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Owner", "Guest" });
        }
    }
}
