using Application.Domain;
using Application.Features;
using FT.CQRS;
using FunctionExtensions.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Domain.Errors;

namespace Innopay.PaymentCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {
        private IBus _messageBus;
        public PaymentStatusController(IBus messageBus) { 
            _messageBus = messageBus;
        }
        [HttpGet]
        public ActionResult<List<PaymentStatusDto>> QueryPaymentStatus()
        {
            var result = _messageBus.Send(new QueryPaymentStatus());
            return Ok(result);
        }      
    }
}
