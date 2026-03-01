namespace BookIt.API.Models.DTO
{
    public class AddPaymentRequestDto
    {
        public Guid payment_id { get; set; } = Guid.NewGuid();

        public Guid BookingId { get; set; }

        public Guid UserId { get; set; }

        public double amount { get; set; }

        public DateTime transaction_date_time { get; set; } = DateTime.Now;

        public Guid EventId { get; set; }

        public string billing_address { get; set; }

        public string transaction_id { get; set; }

        public Boolean isRefunded { get; set; } = false;
    }
}
