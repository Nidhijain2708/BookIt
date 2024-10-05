using BookIt.API.Data;
using BookIt.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookIt.API.Repositories
{
    public class SQLEventRepository : IEventRepository
    {
        private readonly BookItDbContext dbContext;

        public SQLEventRepository(BookItDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await dbContext.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await dbContext.Events.FindAsync(id);
        }
    }
}
