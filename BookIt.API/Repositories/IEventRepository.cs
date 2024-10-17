using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;

namespace BookIt.API.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();

        Task<Event> GetByIdAsync(Guid id);

        Task<List<Event>> GetByFilterAsync(string? filterOn=null,string? filterQuery1=null,string? filterQuery2=null);

        Task<List<Event>> GetBySortAsync(string? sortBy = null,bool? isAscending=true);

        Task<Event> CreateEventAsync(AddEventRequestDto addEventRequestDto);
    }
}
