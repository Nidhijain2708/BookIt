using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;

namespace BookIt.API.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePaymentAsync(AddPaymentRequestDto addPaymentRequestDto);

        Task<Payment> RefundAsync(Guid BookingId);
    }
}
