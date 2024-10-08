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
                    event_id=Guid.Parse("9443d1cc-c761-4984-a1a8-837adfde4387"),
                    event_name="Pre-Diwali Bash",
                    description="Diwali Celebration",
                    location="Delhi",
                    date=new DateOnly(2024,10,1),
                    start_time=new TimeOnly(9,0,0),
                    end_time=new TimeOnly(10,0,0),
                    capacity=250,
                    available_tickets=250,
                    price=1000,
                    artist="Parmish Verma",
                    category="Festive celebration"
                }
            };

            modelBuilder.Entity<Event>().HasData(events);
        }
    }
}
