using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class PaymentStatusDto
    {
        public string PaymentRequestId { get; set;}
        public string Status { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
