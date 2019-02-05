using Microsoft.AspNetCore.Identity;
using TestTemplate.Models;

namespace CoriaTemplate.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}