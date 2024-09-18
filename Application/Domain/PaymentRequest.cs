using Application.Domain.Common;
using Application.PaymentRequest;
using FunctionExtensions.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class PaymentRequest : EntityBase
    {
        public Guid PaymentRequestID { get; private set; }
        public virtual Amount Amount { get; private set; }
        public string Currency { get; private set; }
        public Status Status { get; private set; }
        public string PayerId { get; private set; }
        public string PayeeId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        protected PaymentRequest()
        {

        }
        public PaymentRequest(Guid paymantRequestId, Amount amount,
                            string currency, Status status,
                            string payerID, string payeeID,
                            DateTime createdAt, DateTime updatedAt) : this()

        {
            PaymentRequestID = paymantRequestId;
            Amount = amount;
            Currency = currency;
            Status = status;
            PayerId = payerID;
            PayeeId = payeeID;
            CreatedAt = createdAt;
            UpdateAt = updatedAt;
            RaiseDomainEvent(new PaymentRequestSubmittedEvent(this.PaymentRequestID, status.ToString(), CreatedAt));
        }
        public void UpdatePaymentRequest(Status status, DateTime processedAt)
        {
            Status = status;
            UpdateAt = processedAt;
            RaiseDomainEvent(new PaymentStatusUpdatedEvent(this.PaymentRequestID, status.ToString(), processedAt));
        }
    }
}
