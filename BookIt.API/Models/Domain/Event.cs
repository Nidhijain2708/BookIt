using System.ComponentModel.DataAnnotations;

namespace BookIt.API.Models.Domain
{
    public class Event
    {
        [Key]
        public Guid event_id { get; set; }

        public string event_name { get; set; }

        public string? description { get; set; }

        public string location { get; set; }

        public DateOnly date { get; set; }

        public TimeOnly start_time { get; set; }

        public TimeOnly end_time { get; set; }

        //public int capacity { get; set; }

        //public int available_tickets { get; set; }

        public double price { get; set; }

        public string artist { get; set; }

        public string category { get; set; }
    }
}
