using AutoMapper;
using BookIt.API.Models.DTO;
using BookIt.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;

        public PaymentsController(IPaymentRepository paymentRepository,IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] AddPaymentRequestDto addPaymentRequestDto)
        {
            var paymentDomain=await paymentRepository.CreatePaymentAsync(addPaymentRequestDto);
            return Ok(mapper.Map<PaymentDto>(paymentDomain));
        }

        [HttpPut]
        [Route("Refund")]
        public async Task<IActionResult> Refund([FromBody] RefundRequest request)
        {
            var paymentDomain = await paymentRepository.RefundAsync(request.bookingId);
            return Ok(mapper.Map<PaymentDto>(paymentDomain));
        }
    }
}
