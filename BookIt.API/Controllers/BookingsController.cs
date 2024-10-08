using AutoMapper;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;
using BookIt.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IMapper mapper;
        private readonly IEventRepository eventRepository;
        private readonly UserManager<IdentityUser> userManager;

        public BookingsController(IBookingRepository bookingRepository, IMapper mapper, IEventRepository eventRepository,UserManager<IdentityUser> userManager)
        {
            this.bookingRepository = bookingRepository;
            this.mapper = mapper;
            this.eventRepository = eventRepository;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookingsDomain=await bookingRepository.GetAllAsync();
            var bookingsDto=mapper.Map<List<BookingDto>>(bookingsDomain);
            return Ok(bookingsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var booking=await bookingRepository.GetByIdAsync(id);
            return Ok(mapper.Map<BookingDto>(booking));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BookATicket([FromBody] AddBookingRequestDto addBookingRequestDto)
        {
            // find event using EventId
            var eventDto=await eventRepository.GetByIdAsync(addBookingRequestDto.EventId);

            // calculate total price
            var total_price = addBookingRequestDto.number_of_tickets * eventDto.price;

            Booking booking=mapper.Map<Booking>(addBookingRequestDto);

            // add total_price to this booking domain model
            booking.total_price = total_price;
            // add booking time too
            booking.booking_dateTime = DateTime.Now;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await userManager.FindByIdAsync(userId);
            //booking.UserId = Guid.Parse(userId);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found. User might not be authenticated.");
            }

            // If userId is expected to be a GUID, ensure conversion is valid
            if (!Guid.TryParse(userId, out Guid parsedUserId))
            {
                return BadRequest("User ID is not in a valid format.");
            }

            booking.UserId = parsedUserId;

            booking =await bookingRepository.BookATicketAsync(booking);
            if(booking == null)
            {
                var eventTicketBookedFor =await eventRepository.GetByIdAsync(addBookingRequestDto.EventId);
                var ticketsAvailable = eventTicketBookedFor.available_tickets;
                return BadRequest(new BookingFailedDto()
                {
                    message = $"Can't book {addBookingRequestDto.number_of_tickets}, tickets left: {ticketsAvailable}"
                });
            }

            // return this booking domain after changing it to dto
            var bookingDto=mapper.Map<BookingDto>(booking);
            return CreatedAtAction(nameof(GetById),new { id=bookingDto.booking_id},bookingDto);
        }

        [HttpDelete("{booking_id}")]
        public async Task<IActionResult> DeleteBooking(Guid booking_id)
        {
            var resBooking=await bookingRepository.DeleteBookingAsync(booking_id);

            if (resBooking == null)
            {
                return BadRequest("No booking exist");
            }

            return Ok(mapper.Map<BookingDto>(resBooking));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingRequestDto updateBookingRequestDto)
        {
            var resBooking = await bookingRepository.UpdateBookingAsync(updateBookingRequestDto);
            
            if (resBooking == null)
            {
                var currBooking = await bookingRepository.GetByIdAsync(updateBookingRequestDto.booking_id);
                var eventTicketUpdatedFor = await eventRepository.GetByIdAsync(currBooking.EventId);

                return BadRequest($"No booking found or Number of available tickets are: {eventTicketUpdatedFor.available_tickets} and you are trying to book extra: {updateBookingRequestDto.number_of_tickets-currBooking.number_of_tickets} tickets");
            }

            var response = new UpdateBookingResponseDto(){
                message = "Update Success"
            };

            return Ok(response);
        }
    }
}
