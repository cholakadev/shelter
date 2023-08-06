using System.ComponentModel.DataAnnotations;
using Users.Core.Models.Users.Enums;

namespace Users.Core.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserType Type { get; set; }
    }
}
