using BookIt.API.Data;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;
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
            var eventTicketBookedFor = await dbContext.Events.FindAsync(booking.EventId);
            if ((eventTicketBookedFor.available_tickets - booking.number_of_tickets) >= 0)
            {
                eventTicketBookedFor.available_tickets = (eventTicketBookedFor.available_tickets - booking.number_of_tickets);
                
                await dbContext.Bookings.AddAsync(booking);
                await dbContext.SaveChangesAsync();

                return booking;
            }
            else
            {
                return null;
            }
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

            var eventTicketCancelledFor = await dbContext.Events.FindAsync(Booking.EventId);
            eventTicketCancelledFor.available_tickets += Booking.number_of_tickets;
            
            dbContext.Bookings.Remove(Booking);
            dbContext.SaveChanges();
            return Booking;
        }

        public async Task<Booking> UpdateBookingAsync(UpdateBookingRequestDto updateBookingRequestDto)
        {
            var Booking = await dbContext.Bookings.FindAsync(updateBookingRequestDto.booking_id);

            if(Booking == null)
            {
                return null;
            }
            
            var eventTicketUpdatedFor = await dbContext.Events.FindAsync(Booking.EventId);

            await dbContext.SaveChangesAsync();

            if ((eventTicketUpdatedFor.available_tickets - (updateBookingRequestDto.number_of_tickets-Booking.number_of_tickets)) >= 0 && updateBookingRequestDto.number_of_tickets != 0)
            {
                eventTicketUpdatedFor.available_tickets = (eventTicketUpdatedFor.available_tickets - (updateBookingRequestDto.number_of_tickets - Booking.number_of_tickets));

                Booking.number_of_tickets = updateBookingRequestDto.number_of_tickets;
                Booking.total_price = updateBookingRequestDto.number_of_tickets * (eventTicketUpdatedFor.price);
                await dbContext.SaveChangesAsync();
            }
            else if (updateBookingRequestDto.number_of_tickets == 0)
            {
                var eventTicketCancelledFor = await dbContext.Events.FindAsync(Booking.EventId);
                eventTicketCancelledFor.available_tickets += Booking.number_of_tickets;

                dbContext.Bookings.Remove(Booking);
                dbContext.SaveChanges();
                return Booking;
            }
            else if((eventTicketUpdatedFor.available_tickets - (updateBookingRequestDto.number_of_tickets - Booking.number_of_tickets)) < 0)
            {
                return null;
            }
            
            dbContext.Bookings.Update(Booking);
            dbContext.SaveChanges();
            return Booking;
        }
    }
}
