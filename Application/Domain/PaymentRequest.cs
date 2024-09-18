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
        public PaymentRequest( Amount amount, string currency, Status status,string payerID, string payeeID, DateTime createdAt, DateTime updatedAt):this()
        {
            Amount = amount;
            Currency = currency;
            Status = status;
            PayerId = payerID;
            PayeeId = payeeID;
            CreatedAt = createdAt;
            UpdateAt = updatedAt;
            RaiseDomainEvent(new PaymentStatusCreatedEvent(this.Id, status.ToString(), CreatedAt));
        }
        public void UpdatePaymentRequest(Status status, DateTime processedAt)
        {
            Status = status;
           // RaiseDomainEvent(new PaymentStatusChangeEvent(this.Id, status.ToString(), processedAt));        
        }
    }
}
