﻿using Application.Infrastrucure;
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
    public class PaymentStatusCreatedEvent : IEvent
    {
        public Guid PaymentRequestId { get; }
        public string PaymentStatus { get; }
        public DateTime ProcessedAt { get; }
        public PaymentStatusCreatedEvent(Guid paymentRequestId, string paymentStatus, DateTime processedAt)
        {
            PaymentRequestId = paymentRequestId;
            PaymentStatus = paymentStatus;
            ProcessedAt = processedAt;
        }
        [EventLog]
        internal sealed class PaymentStatusCreatedEventHandler : IEventHandler<PaymentStatusCreatedEvent>
        {
            private PaymentStatusRepository _paymentStatusRepository;
           
            public PaymentStatusCreatedEventHandler(PaymentStatusRepository paymentStatusRepository)
            {
                _paymentStatusRepository = paymentStatusRepository;
            }

            public Result Handle(PaymentStatusCreatedEvent domainEvent)
            {
                _paymentStatusRepository.InsertPaymentStatus(domainEvent.PaymentRequestId, domainEvent.PaymentStatus, domainEvent.ProcessedAt);
                return Result.Success();
            }
        }
    }
}
