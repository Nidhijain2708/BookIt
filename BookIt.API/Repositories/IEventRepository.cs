using BookIt.API.Models.Domain;

namespace BookIt.API.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();

        Task<Event> GetByIdAsync(Guid id);
    }
}
