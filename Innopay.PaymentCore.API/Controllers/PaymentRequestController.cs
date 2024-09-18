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
        [HttpGet]
        public ActionResult<List<PaymentStatusDto>> GetAll()
        {
            GetPaymentStatusListQuery query = new GetPaymentStatusListQuery();
            var result = _messageBus.Send(query);
            return Ok(result);
        }
        [HttpPost("createpayment")]
        public IActionResult CreatePayment(string payerID, string payeeID, long amount)
        {
            _messageBus.Send(new CreatePaymentCommand(Guid.NewGuid(), payerID, payeeID, amount));
            return Ok();
        }
        [HttpPost("updatepayment")]
        public IActionResult UpdatePayment(string paymentrequestId,Status status)
        {    
            _messageBus.Send(new UpdatePaymentStatusCommand(Guid.Parse(paymentrequestId), status));
            return Ok();
        }
        
    }
}
