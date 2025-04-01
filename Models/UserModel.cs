using System.ComponentModel.DataAnnotations;

namespace OuathAuthentication.Models
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string LastName { get; set; } = default!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        public string Password { get; set; } = default!;
    }
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = default!;
    }
    public class UserResponseModel
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; } 
        public string Role { get; set; }
        public int TimesOfLogin { get; set; }
        public DateTime LastLoginAt { get; set; }
        public string Token { get; set; }
    }
}
