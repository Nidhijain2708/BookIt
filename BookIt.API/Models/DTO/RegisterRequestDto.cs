using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookIt.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 characters long.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
    }
}
