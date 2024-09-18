using Application.Infrastrucure;
using FT.CQRS;
using FunctionExtensions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentRequest
{
    public class PaymentStatusCreatedEvent : IEvent
    {
        public int PaymentRequestId { get; }
        public string PaymentStatus { get; }
        public DateTime ProcessedAt { get; }
        public PaymentStatusCreatedEvent(int paymentRequestId, string paymentStatus, DateTime processedAt)
        {
            PaymentRequestId = paymentRequestId;
            PaymentStatus = paymentStatus;
            ProcessedAt = processedAt;
        }
        internal sealed class PaymentStatusCreatedEventHandler : IEventHandler<PaymentStatusCreatedEvent>
        {
            private PaymentStatusRepository _paymentStatusRepository;
            public PaymentStatusCreatedEventHandler(PaymentStatusRepository paymentStatusRepository)
            {
                _paymentStatusRepository = paymentStatusRepository;
            }

            public Result Handle(PaymentStatusCreatedEvent domainEvent)
            {
                return Result.Success();
            }
        }
        internal sealed class PaymentStatusCreatedEvent2Handler : IEventHandler<PaymentStatusCreatedEvent>
        {
            private PaymentStatusRepository _paymentStatusRepository;
            public PaymentStatusCreatedEvent2Handler(PaymentStatusRepository paymentStatusRepository)
            {
                _paymentStatusRepository = paymentStatusRepository;
            }

            public Result Handle(PaymentStatusCreatedEvent domainEvent)
            {
                return Result.Success();
            }
        }
    }
}
