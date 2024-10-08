using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;

namespace BookIt.API.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> BookATicketAsync(Booking booking);

        Task<Booking> GetByIdAsync(Guid id);

        Task<List<Booking>> GetAllAsync();

        Task<Booking> DeleteBookingAsync(Guid id);

        Task<Booking> UpdateBookingAsync(UpdateBookingRequestDto updateBookingRequestDto);
    }
}
