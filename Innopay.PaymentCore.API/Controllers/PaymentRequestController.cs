using Application.Domain;
using Application.PaymentRequest;
using FT.CQRS;
using FunctionExtensions.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("createpayment")]
        public IActionResult CreatePayment(string payerID, string payeeID, long amount)
        {
            CreatePaymentCommand createPaymentCommand = new CreatePaymentCommand(payerID, payeeID, amount);
            _messageBus.Send(createPaymentCommand);
            return Ok();
        }
        [HttpPost("updatepayment")]
        public IActionResult UpdatePayment(Status status)
        {
            return Ok();
        }
    }
}
