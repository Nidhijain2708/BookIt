using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.Domain
{
    public class Booking
    {
        [Key]
        public Guid booking_id { get; set; }

        public Guid UserId { get; set; }

        public int number_of_tickets { get; set; }

        public double total_price { get; set; }

        public DateTime booking_dateTime { get; set; }

        public Guid EventId { get; set; }

        // Navigation properties

        public Event Event { get; set; }
    }
}
