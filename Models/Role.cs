using System.Collections.Generic;
using CoriaTemplate.Models;
using Microsoft.AspNetCore.Identity;

namespace TestTemplate.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}