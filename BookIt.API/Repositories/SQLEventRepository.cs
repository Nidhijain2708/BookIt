using AutoMapper;
using BookIt.API.Data;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BookIt.API.Repositories
{
    public class SQLEventRepository : IEventRepository
    {
        private readonly BookItDbContext dbContext;
        private readonly IMapper mapper;

        public SQLEventRepository(BookItDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await dbContext.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await dbContext.Events.FindAsync(id);
        }

        public async Task<List<Event>> GetByFilterAsync(string? filterOn=null, string? filterQuery1=null, string? filterQuery2=null)
        {
            IQueryable<Event> events = dbContext.Events;

            // filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery1) == false && string.IsNullOrWhiteSpace(filterQuery2)==false)
            {
                if (filterOn.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    DateOnly startDate = DateOnly.Parse(filterQuery1);
                    DateOnly endDate = DateOnly.Parse(filterQuery2);
                    events = events.Where(x => x.date >= startDate && x.date <= endDate);
                    return await events.ToListAsync();
                }
            }
            else if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery1)==false)
            {
                if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.category.ToLower().Contains(filterQuery1.ToLower()));
                    return await events.ToListAsync();
                }
            }

            return null;
        }

        public async Task<List<Event>> GetBySortAsync(string? sortBy = null, bool? isAscending = true)
        {
            var events=await dbContext.Events.ToListAsync();

            if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
            {
                if (isAscending == true)
                {
                    events = events.OrderBy(x => x.date).ToList();
                }
                else
                {
                    events = events.OrderByDescending(x => x.date).ToList();
                }
            }
            else if(sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
            {
                if (isAscending == true)
                {
                    events = events.OrderBy(x => x.price).ToList();
                }
                else
                {
                    events = events.OrderByDescending(x => x.price).ToList();
                }
            }

            return events;
        }

        public async Task<Event> CreateEventAsync(AddEventRequestDto addEventRequestDto)
        {
            var eventDomain = mapper.Map<Event>(addEventRequestDto);

            string[] categoryList = new[] { "Music", "Theater", "Comedy", "Sports", "Conference", "Workshop", "Food Fest", "Game", "Acting", "Competition" };
            if (!categoryList.Contains(eventDomain.category))
            {
                throw new ArgumentException("Category Should be from the predefined list [\"Music\", \"Theater\", \"Comedy\", \"Sports\", \"Conference\", \"Workshop\", \"Food Fest\", \"Game\", \"Acting\", \"Competition\"].");
            }

            List<string> filePaths = new List<string>();
            int i = 0;
            foreach (var image in addEventRequestDto.images)
            {
                if (image.Length > 5000000)
                {
                    throw new ArgumentException("Image size more than 5MB, All images should be smaller than 5MB.");
                }

                var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(addEventRequestDto.fileNames[i].Replace(" ", "_"))}";
                var localFilePath = Path.Combine(imagesFolder, uniqueFileName);
                i++;
                filePaths.Add($"/Images/{uniqueFileName}");
                
                using var stream = new FileStream(localFilePath, FileMode.Create);
                await image.CopyToAsync(stream);
            }
            eventDomain.filePaths = filePaths.ToArray();

            await dbContext.Events.AddAsync(eventDomain);
            await dbContext.SaveChangesAsync();

            return eventDomain;
        }
    }
}
