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
                    event_id=Guid.Parse("897a64d7-94b2-428d-9548-1d9c4f78c65c"),
                    event_name="Horn OK Please",
                    description="The Happiest Food Festival 13.0",
                    location="JLN Stadium, Gate No. 14, Delhi",
                    date=new DateOnly(2024,11,16),
                    start_time=new TimeOnly(12,0,0),
                    end_time=new TimeOnly(16,0,0),
                    capacity=300,
                    available_tickets=300,
                    price=299,
                    artist="Kunal Kapur",
                    category="Food Fest"
                },
                new Event()
                {
                    event_id=Guid.Parse("bcc8c341-ece2-4fec-81ca-3b7f31f3c195"),
                    event_name="Thrifty X Genz Strangers Meet",
                    description="Discover Connections in the Most Exciting Way (18+)",
                    location="Delhi",
                    date=new DateOnly(2024,12,19),
                    start_time=new TimeOnly(13,0,0),
                    end_time=new TimeOnly(19,0,0),
                    capacity=100,
                    available_tickets=100,
                    price=599,
                    artist="Neha Dhupia",
                    category="Game"
                },
                new Event()
                {
                    event_id=Guid.Parse("e1772253-33d7-4cd0-a49b-aeeeae35ae29"),
                    event_name="'Out Of Control' with Blue Family",
                    description="Can't wait to listen to Darshan Raval's new album 'Out Of Control'?",
                    location="Saket Social, Delhi",
                    date=new DateOnly(2025,1,10),
                    start_time=new TimeOnly(18,0,0),
                    end_time=new TimeOnly(21,0,0),
                    capacity=200,
                    available_tickets=200,
                    price=2000,
                    artist="Darshan Raval",
                    category="Music"
                },
                new Event()
                {
                    event_id=Guid.Parse("b60c1f30-a2cc-4453-ae8e-d1ba4677d64e"),
                    event_name="Kisi Ko Batana Mat Ft. Anubhav Singh Bassi",
                    description="After the great success of his previous show Bas kar bassi, Anubhav Singh Bassi is coming back to perform live on stage.",
                    location="Delhi",
                    date=new DateOnly(2025,2,2),
                    start_time=new TimeOnly(11,0,0),
                    end_time=new TimeOnly(12,0,0),
                    capacity=70,
                    available_tickets=70,
                    price=799,
                    artist="Anubhav Singh Bassi",
                    category="Comedy"
                },
                new Event()
                {
                    event_id=Guid.Parse("1ccab91a-f452-417b-bb72-787aff994af8"),
                    event_name="It Was All A Dream",
                    description="Experience the magic of Karaan Aujla live on his It Was All A Dream World Tour!",
                    location="Mumbai",
                    date=new DateOnly(2025,3,1),
                    start_time=new TimeOnly(19,0,0),
                    end_time=new TimeOnly(20,0,0),
                    capacity=200,
                    available_tickets=200,
                    price=1999,
                    artist="Karan Aujla",
                    category="Music"
                },
                new Event()
                {
                    event_id=Guid.Parse("7a08f46b-f298-47b5-a39e-f331f8ddc575"),
                    event_name="Main Shayar Toh Nahi ft Manhar Seth",
                    description="Manhar Seth's pomedy set 'Main Shayar Toh Nahi' along with some crowdwork",
                    location="Mehendi Navaz Jung Hall: Ahemdabad",
                    date=new DateOnly(2025,1,2),
                    start_time=new TimeOnly(9,0,0),
                    end_time=new TimeOnly(10,0,0),
                    capacity=60,
                    available_tickets=60,
                    price=399,
                    artist="Manhar Seth",
                    category="Comedy"
                },
                new Event()
                {
                    event_id=Guid.Parse("afb4c67b-f72c-4486-b86f-5690e4b6937f"),
                    event_name="International Clown Festival - Chennai",
                    description="Join Flubber, the renowned award-winning clown and creator of India's International Clown Festival, on another exhilarating adventure.",
                    location="Sri Mutha Venkatasubba Rao Concert Hall: Chennai",
                    date=new DateOnly(2024,12,4),
                    start_time=new TimeOnly(13,0,0),
                    end_time=new TimeOnly(15,0,0),
                    capacity=120,
                    available_tickets=120,
                    price=800,
                    artist="John Flubber",
                    category="Acting"
                },
                new Event()
                {
                    event_id=Guid.Parse("a5807ecd-be61-4845-8645-c88ac1e79e4b"),
                    event_name="Cocomelon Comes To Ahmedabad!",
                    description="CoComelon - Family & Friends are coming to India for the first time…for a Play Date!",
                    location="Palladium Mall: Ahemdabad",
                    date=new DateOnly(2024,11,2),
                    start_time=new TimeOnly(9,0,0),
                    end_time=new TimeOnly(10,0,0),
                    capacity=50,
                    available_tickets=50,
                    price=100,
                    artist="All kids who join us",
                    category="Competition"
                },
                new Event()
                {
                    event_id=Guid.Parse("b8c62ec5-70b6-4618-a289-d357245c5dc0"),
                    event_name="Gaurav Gupta Live",
                    description="Diwali Celebration",
                    location="Talkatora Stadium Delhi",
                    date=new DateOnly(2024,12,10),
                    start_time=new TimeOnly(10,0,0),
                    end_time=new TimeOnly(11,0,0),
                    capacity=60,
                    available_tickets=60,
                    price=799,
                    artist="Gaurav Gupta",
                    category="Comedy"
                }
            };

            modelBuilder.Entity<Event>().HasData(events);
        }
    }
}
