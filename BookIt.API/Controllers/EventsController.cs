using BookIt.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookIt.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using BookIt.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using BookIt.API.Models.Domain;
using MimeKit;
using Org.BouncyCastle.Utilities;

namespace BookIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;
        private readonly BookItDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EventsController(IEventRepository eventRepository, IMapper mapper,BookItDbContext dbContext,IWebHostEnvironment webHostEnvironment) 
        {
            this.eventRepository = eventRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database - Domain model
            var eventsDomain = await eventRepository.GetAllAsync();

            // Map Domain models to DTOs
            var eventsDto = mapper.Map<List<EventDto>>(eventsDomain);

            // Return DTOs
            return Ok(eventsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var eventDomain = await eventRepository.GetByIdAsync(id);
            return Ok(mapper.Map<EventDto>(eventDomain));
        }

        [HttpGet]
        [Route("GetByFilter")]
        public async Task<IActionResult> GetByFilter([FromQuery] string? filterOn, [FromQuery] string? filterQuery1, [FromQuery] string? filterQuery2)
        {
            var eventsModel=await eventRepository.GetByFilterAsync(filterOn, filterQuery1, filterQuery2);

            if(eventsModel == null)
            {
                return BadRequest("Filter by date or category, while in date provide range of date.");
            }

            return Ok(mapper.Map<List<EventDto>>(eventsModel));
        }

        [HttpGet]
        [Route("GetBySort")]
        public async Task<IActionResult> GetBySort([FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var eventsModel = await eventRepository.GetBySortAsync(sortBy,isAscending);

            return Ok(mapper.Map<List<EventDto>>(eventsModel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromForm] AddEventRequestDto addEventRequestDto)
        {
            try
            {
                var createdEvent = await eventRepository.CreateEventAsync(addEventRequestDto);
                return Ok(mapper.Map<EventDto>(createdEvent));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the event.");
            }
        }
    }
}
