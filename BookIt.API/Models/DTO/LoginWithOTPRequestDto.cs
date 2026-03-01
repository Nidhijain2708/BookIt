namespace BookIt.API.Models.DTO
{
    public class LoginWithOTPRequestDto
    {
        public string code {  get; set; }

        public string email { get; set; }
    }
}
