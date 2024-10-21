using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.Domain
{
    public class User
    {
        [Key]
        public Guid user_id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

        public string phone_number { get; set; }

        public string password { get; set; }

        public string preferred_language { get; set; }

        public string preferred_currency { get; set; }

        public string profile_pic_path { get; set; }
    }
}
