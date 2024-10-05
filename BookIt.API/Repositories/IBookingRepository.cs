using BookIt.API.Models.Domain;

namespace BookIt.API.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> BookATicketAsync(Booking booking);

        Task<Booking> GetByIdAsync(Guid id);

        Task<List<Booking>> GetAllAsync();

        Task<Booking> DeleteBookingAsync(Guid id);
    }
}
