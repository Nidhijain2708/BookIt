using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookIt.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Key]
        [BindNever]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 characters long.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        public string? FileName { get; set; }

        [Required]
        public string PreferredLanguage { get; set; }

        [Required]
        public string PreferredCurrency { get; set; }
    }
}
