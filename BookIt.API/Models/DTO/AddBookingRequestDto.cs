namespace BookIt.API.Models.DTO
{
    public class AddBookingRequestDto
    {
        public int number_of_tickets { get; set; }

        public Guid EventId { get; set; }
    }
}
