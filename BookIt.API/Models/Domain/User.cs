using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.Domain
{
    public class User
    {
        [Key]
        public Guid user_id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string phone_number { get; set; }

        public string password { get; set; }
    }
}
