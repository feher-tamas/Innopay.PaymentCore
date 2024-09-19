using Application.Domain;
using Application.Infrastrucure;
using FT.CQRS;
using FT.CQRS.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class QueryPaymentStatus : IQuery<List<PaymentStatusDto>>
    {
        [QueryLog]
        internal sealed class QueryPaymentStatusHandler : IQueryHandler<QueryPaymentStatus, List<PaymentStatusDto>>
        {
            private PaymentStatusRepository _paymentStatusRepository;
            public QueryPaymentStatusHandler(PaymentStatusRepository paymentStatusRepository)
            {
                _paymentStatusRepository = paymentStatusRepository;
            }
            public List<PaymentStatusDto> Handle(QueryPaymentStatus query)
            {
                return _paymentStatusRepository.GetAll();
            }
        }
    }
}
