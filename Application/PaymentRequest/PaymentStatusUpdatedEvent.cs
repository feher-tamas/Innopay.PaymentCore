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
    public class PaymentStatusUpdatedEvent : IEvent
    {
        public Guid PaymentRequestId { get; }
        public string PaymentStatus { get; }
        public DateTime ProcessedAt { get; }
        public PaymentStatusUpdatedEvent(Guid paymentRequestId, string paymentStatus, DateTime processedAt)
        {
            PaymentRequestId = paymentRequestId;
            PaymentStatus = paymentStatus;
            ProcessedAt = processedAt;
        }
        [EventLog]
        internal sealed class PaymentStatusUpdatedEventHandler : IEventHandler<PaymentStatusUpdatedEvent>
        {
            private PaymentStatusRepository _paymentStatusRepository;
            public PaymentStatusUpdatedEventHandler(PaymentStatusRepository paymentStatusRepository)
            {
                _paymentStatusRepository = paymentStatusRepository;
            }

            public Result Handle(PaymentStatusUpdatedEvent domainEvent)
            {
                _paymentStatusRepository.UpdatePaymentStatus(domainEvent.PaymentRequestId, domainEvent.PaymentStatus, domainEvent.ProcessedAt);
                return Result.Success();
            }
        }
    }
}
