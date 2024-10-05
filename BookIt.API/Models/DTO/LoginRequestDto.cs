using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.DTO
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters long.")]
        public string Password { get; set; }
    }
}
