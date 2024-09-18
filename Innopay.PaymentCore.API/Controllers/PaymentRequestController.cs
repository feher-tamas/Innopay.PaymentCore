using Application.Domain;
using Application.PaymentRequest;
using FT.CQRS;
using FunctionExtensions.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Domain.Errors;

namespace Innopay.PaymentCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestController : ControllerBase
    {
        private IBus _messageBus;
        public PaymentRequestController(IBus messageBus) { 
            _messageBus = messageBus;
        }
        [HttpPost("SubmitPaymentRequest")]
        public IActionResult CreatePayment(string payerID, string payeeID, long amount)
        {
            var result = _messageBus.Send(new SubmitPaymentRequestCommand(Guid.NewGuid(), payerID, payeeID, amount));
            if (result.IsFailure) {
                return BadRequest(result.Error);
            }
            return Ok();
        }
        [HttpPost("UpdatePaymentStatus")]
        public IActionResult UpdatePaymentStatus(Guid paymentrequestId,Status status)
        {    
           var result= _messageBus.Send(new UpdatePaymentStatusCommand(paymentrequestId, status));
            if (result.IsFailure) {
                return NotFound(result.Error);
            }
            return Ok();
        }
        
    }
}
