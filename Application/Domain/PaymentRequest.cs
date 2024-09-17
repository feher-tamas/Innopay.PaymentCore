using Application.Domain.Common;
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
        public Guid PayerID { get; private set; }
        public Guid PayeeId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        protected PaymentRequest()
        {

        }
        public PaymentRequest( Amount amount, string currency, Status status,Guid payerID, Guid payeeID, DateTime createdAt, DateTime updatedAt):this()
        {
            Amount = amount;
            Currency = currency;
            Status = status;
            PayerID = payerID;
            CreatedAt = createdAt;
            UpdateAt = updatedAt;
        }
        public void UpdatePaymentRequest(Status status, DateTime processedAt)
        {
            RaiseDomainEvent(new PaymentStatusChangeEvent(this.Id, status.ToString(), processedAt));
            Status = status; 
        }
    }
}
