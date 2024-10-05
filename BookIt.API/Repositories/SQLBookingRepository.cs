using BookIt.API.Data;
using BookIt.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookIt.API.Repositories
{
    public class SQLBookingRepository : IBookingRepository
    {
        private readonly BookItDbContext dbContext;

        public SQLBookingRepository(BookItDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Booking> BookATicketAsync(Booking booking)
        {
            await dbContext.Bookings.AddAsync(booking);
            await dbContext.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking> GetByIdAsync(Guid id)
        {
            return await dbContext.Bookings.FindAsync(id);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await dbContext.Bookings.ToListAsync();
        }

        public async Task<Booking> DeleteBookingAsync(Guid id)
        {
            var Booking=await dbContext.Bookings.FindAsync(id);
            if (Booking == null)
            {
                return null;
            }
            dbContext.Bookings.Remove(Booking);
            dbContext.SaveChanges();
            return Booking;
        }
    }
}
