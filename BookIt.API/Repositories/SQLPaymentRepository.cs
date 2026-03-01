using AutoMapper;
using BookIt.API.Data;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BookIt.API.Repositories
{
    public class SQLPaymentRepository: IPaymentRepository
    {
        private readonly IMapper mapper;
        private readonly BookItDbContext dbContext;

        public SQLPaymentRepository(IMapper mapper, BookItDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<Payment> CreatePaymentAsync(AddPaymentRequestDto addPaymentRequestDto)
        {
            var paymentDomain = mapper.Map<Payment>(addPaymentRequestDto);
            await dbContext.Payments.AddAsync(paymentDomain);
            await dbContext.SaveChangesAsync();
            return paymentDomain;
        }

        public async Task<Payment> RefundAsync(Guid BookingId)
        {
            // Find the payment record by BookingId
            var paymentDomain = await dbContext.Payments
                .FirstOrDefaultAsync(p => p.BookingId == BookingId);

            // Check if a matching record was found
            if (paymentDomain != null)
            {
                paymentDomain.isRefunded = true;
                await dbContext.SaveChangesAsync();
            }

            return paymentDomain; // Will return null if no matching record was found
        }
    }
}
