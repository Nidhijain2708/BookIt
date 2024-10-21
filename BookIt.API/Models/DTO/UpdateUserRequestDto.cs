using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.DTO
{
    public class UpdateUserRequestDto
    {
        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public string? email { get; set; }

        public string? currentPassword { get; set; }

        public string? newPassword { get; set; }

        public string? phone_number { get; set; }

        public string? preferred_language { get; set; }

        public string? preferred_currency { get; set; }

        public IFormFile? profilePicture { get; set; }

        public string? filename { get; set; }
    }
}
