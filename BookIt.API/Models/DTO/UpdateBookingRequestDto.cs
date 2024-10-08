namespace BookIt.API.Models.DTO
{
    public class UpdateBookingRequestDto
    {
        public Guid booking_id { get; set; }
        public int number_of_tickets { get; set; }
    }
}
