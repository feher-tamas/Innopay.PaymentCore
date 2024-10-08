﻿using Application.Infrastrucure;
using FT.CQRS;
using FT.CQRS.Decorators;
using FunctionExtensions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class PaymentRequestCreatedEvent : IEvent
    {
        public Guid PaymentRequestId { get; }
        public string PaymentStatus { get; }
        public DateTime ProcessedAt { get; }
        public PaymentRequestCreatedEvent(Guid paymentRequestId, string paymentStatus, DateTime processedAt)
        {
            PaymentRequestId = paymentRequestId;
            PaymentStatus = paymentStatus;
            ProcessedAt = processedAt;
        }
        [EventLog]
        internal sealed class PaymentRequestSubmittedEventHandler : IEventHandler<PaymentRequestCreatedEvent>
        {
            private PaymentStatusRepository _paymentStatusRepository;
           
            public PaymentRequestSubmittedEventHandler(PaymentStatusRepository paymentStatusRepository)
            {
                _paymentStatusRepository = paymentStatusRepository;
            }

            public Result Handle(PaymentRequestCreatedEvent domainEvent)
            {
                _paymentStatusRepository.InsertPaymentStatus(domainEvent.PaymentRequestId, domainEvent.PaymentStatus, domainEvent.ProcessedAt);
                return Result.Success();
            }
        }
    }
}
