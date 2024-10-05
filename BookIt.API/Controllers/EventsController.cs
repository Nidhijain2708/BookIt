using BookIt.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookIt.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using BookIt.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace BookIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;

        public EventsController(IEventRepository eventRepository, IMapper mapper) 
        {
            this.eventRepository = eventRepository;
            this.mapper = mapper;
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
    }
}
