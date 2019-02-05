using System;
using System.ComponentModel.DataAnnotations;

namespace TestDotNetCoreTemplate.Models.Dto
{
    public class UserRegistrationDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters")]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }

        public UserRegistrationDto()
        {
            Created = DateTime.UtcNow;
        }
    }
}