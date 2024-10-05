using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.DTO
{
    public class UpdateUserRequestDto
    {
        public string? name { get; set; }

        public string? email { get; set; }

        public string? currentPassword { get; set; }

        public string? newPassword { get; set; }

        public string? phone_number { get; set; }
    }
}
