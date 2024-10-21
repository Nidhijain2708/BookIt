namespace BookIt.API.Models.DTO
{
    public class UserDto
    {
        public string user_id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

        public string phone_number { get; set; }

        public string preferred_language { get; set; }

        public string preferred_currency { get; set; }

        public string profile_pic_path { get; set; }
    }
}
