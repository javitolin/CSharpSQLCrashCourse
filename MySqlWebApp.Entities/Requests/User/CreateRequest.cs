using System.ComponentModel.DataAnnotations;

namespace MySqlWebApp.Entities.Requests.User
{
    public class CreateRequest
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(Role))]
        public string Role { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /*
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
         */
    }
}
