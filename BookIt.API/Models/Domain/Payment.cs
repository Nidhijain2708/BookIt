using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.Domain
{
    public class Payment
    {
        [Key]
        public Guid payment_id { get; set; }

        public Guid BookingId { get; set; }

        public Guid UserId { get; set; }

        public double amount { get; set; }

        public DateTime transaction_date_time { get; set; }

        public Guid EventId { get; set; }

        public string billing_address { get; set; }

        public string transaction_id { get; set; }

        public Boolean isRefunded { get; set; }

        // Navigation properties

        public Event Event { get; set; }

        public User User {  get; set; }
    }
}
