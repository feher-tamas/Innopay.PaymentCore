using FT.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public sealed class PaymentStatusChangeEvent : IEvent
    {
    public int PaymentRequestId { get; }
    public string PaymentStatus { get; }
    public DateTime ProcessedAt { get; }
    public PaymentStatusChangeEvent(int paymentRequestId, string paymentStatus, DateTime processedAt)
    {
        paymentRequestId = paymentRequestId;
        paymentStatus = paymentStatus;
        processedAt = processedAt;

    }
}
}
