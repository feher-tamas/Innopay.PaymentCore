using Application.Domain;
using Application.Infrastrucure;
using FT.CQRS;
using FT.CQRS.Decorators;
using FunctionExtensions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentRequest
{
    public class UpdatePaymentStatusCommand :ICommand
    {
        public Guid PaymentRequsetId { get; }
        public  Status Status { get; }

       
        public UpdatePaymentStatusCommand(Guid paymentRequestId, Status status)
        {
            PaymentRequsetId = paymentRequestId;
            Status = status;
        }
        [CommandLog]
        internal sealed class UpdatePaymentStatusCommandHandler : ICommandHandler<UpdatePaymentStatusCommand>
        {
            PaymentRequestContext _context;

            public UpdatePaymentStatusCommandHandler(PaymentRequestContext context)
            {
                _context = context;
            }
            public Result Handle(UpdatePaymentStatusCommand command)
            {
                var result = _context.PaymentRequests
                     .FirstOrDefault(x => x.PaymentRequestID == command.PaymentRequsetId);
                if (result == null) {
                    return Result.Failure(Errors.General.NotFound("PaymentRequest", command.PaymentRequsetId).Code);
                }
                result.UpdatePaymentRequest(command.Status, DateTime.Now);
             
                _context.SaveChanges();
                return Result.Success();
            }
        }
    }
}
