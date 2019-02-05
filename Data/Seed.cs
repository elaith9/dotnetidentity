using System.Collections.Generic;
using System.Linq;
using TestTemplate.Models;
using Microsoft.AspNetCore.Identity;

namespace TestDotNetCoreTemplate.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            // return if users were already created
            if (_userManager.Users.Any()) return;

            var roles = new List<Role>
            {
                new Role{ Name = "admin"},
                new Role{ Name = "user" }
            };

            // roles.ForEach(role => _roleManager.CreateAsync(role).Wait());

            // returning async void causes some issues with Sql Server and MySql, that's why we use Wait()
            // var admin = new User { UserName = "admin" };
            // _userManager.CreateAsync(admin, "admin").Wait();
            // _userManager.AddToRoleAsync(admin, "admin");

            // var user = new User { UserName = "user" };
            // _userManager.CreateAsync(user, "user").Wait();
            // _userManager.AddToRoleAsync(user, "user");
        }
    }
}