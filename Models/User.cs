using System.Collections.Generic;
using CoriaTemplate.Models;
using Microsoft.AspNetCore.Identity;

namespace TestTemplate.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}