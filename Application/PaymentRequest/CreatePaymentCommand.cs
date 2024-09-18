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
    public class CreatePaymentCommand :ICommand
    {
        public Guid PaymentRequsetId { get; }
        public string PayerID { get; }
        public string PayeeID { get; }
        public decimal Amount { get;}
       
        public CreatePaymentCommand(Guid paymentRequestId , string payerID, string payeeID, decimal amount)
        {
            PaymentRequsetId = paymentRequestId;
            PayerID = payerID;
            PayeeID = payeeID;
            Amount = amount;
        }
        [CommandLog]
        internal sealed class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand>
        {
            PaymentRequestContext _context;

            public CreatePaymentCommandHandler(PaymentRequestContext context)
            {
                _context = context;
            }
            public Result Handle(CreatePaymentCommand command)
            {
                Result<Amount,Error> amount = Domain.Amount.Create(command.Amount);
                if (amount.IsFailure)
                {
                    return Result.Failure(amount.Error.Message);
                }
                Domain.PaymentRequest paymentrequest = new Domain.PaymentRequest(command.PaymentRequsetId, amount.Value, "HUF", Status.Pending, command.PayerID, command.PayeeID, DateTime.Now, DateTime.Now);
                _context.Add(paymentrequest);
                _context.SaveChanges();
                return Result.Success();
            }
        }
    }
}
