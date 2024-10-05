using BookIt.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookIt.API.Data
{
    public class BookItDbContext : DbContext
    {
        public BookItDbContext(DbContextOptions<BookItDbContext> DbContextOptions) : base(DbContextOptions)
        {

        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        // To seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for events
            var events = new List<Event>()
            {
                new Event()
                {
                    event_id=Guid.Parse("f09ce828-2207-4927-be79-459bf96ee99f"),
                    event_name="Comedy Nights",
                    description="Laughter therapy",
                    location="Noida",
                    date=new DateOnly(2024,10,10),
                    start_time=new TimeOnly(9,0,0),
                    end_time=new TimeOnly(10,0,0),
                    price=2000,
                    artist="Gaurav Kapoor",
                    category="Comedy Shows"
                }
            };

            modelBuilder.Entity<Event>().HasData(events);
        }
    }
}
