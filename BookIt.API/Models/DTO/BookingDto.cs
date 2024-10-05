namespace BookIt.API.Models.DTO
{
    public class BookingDto
    {
        public Guid booking_id { get; set; }

        public Guid UserId { get; set; }

        public int number_of_tickets { get; set; }

        public double total_price { get; set; }

        public DateTime booking_dateTime { get; set; }

        public Guid EventId { get; set; }
    }
}
