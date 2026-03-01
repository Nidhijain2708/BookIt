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

        public DbSet<User> Users { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
