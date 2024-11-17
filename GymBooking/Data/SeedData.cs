using GymBooking.Data;
using GymBooking.Models;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.Formatters;

namespace GymBooking.Data
{
    public class SeedData
    {
        private static ApplicationDbContext context = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;

        public static async Task Init(ApplicationDbContext _context, IServiceProvider services)
        {
            context = _context;
            if (context.Roles.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            //var roleNames = new[] { "User", "Admin" };
            //var adminEmail = "admin@admin.com";
            //var userEmail = "user@user.com";




            if (await roleManager.RoleExistsAsync("Admin") == false)
            {
                var role = new IdentityRole { Name = "Admin" };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }


            //await AddRolesAsync(roleNames);




            //var admin = await AddAccountAsync(adminEmail, "Admin", "Adminsson", 9001, "P@55w.rd");
            //var user = await AddAccountAsync(userEmail, "User", "Usersson", 25, "Pa55w.rd");

            //var admin = await AddAccountAsync(adminEmail, "P@55w.rd");
            var user = await AddAccountAsync("admin@Gymbokning.se", "4Ulven¤");


            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                var result = await userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }


            //await AddUserToRoleAsync(admin, "Admin");
            //await AddUserToRoleAsync(user, "User");

        }

        private static async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            }
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        //private static async Task<ApplicationUser> AddAccountAsync(string accountEmail, string fName, string lName, int age, string pw)
        private static async Task<ApplicationUser> AddAccountAsync(string accountEmail, string pw)

        {
            var found = await userManager.FindByEmailAsync(accountEmail);

            if (found != null) return null!;

            var user = new ApplicationUser
            {
                UserName = accountEmail,
                Email = accountEmail,
                //FirstName = fName,
                //LastName = lName,
                //Age = age,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, pw);

            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return user;
        }
    }
}
