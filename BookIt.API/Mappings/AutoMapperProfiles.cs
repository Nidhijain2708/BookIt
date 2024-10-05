using AutoMapper;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;

namespace BookIt.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<AddBookingRequestDto, Booking>().ReverseMap();
            CreateMap<BookingDto, Booking>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
