using Application.Domain;
using Application.Infrastrucure;
using FT.CQRS;
using FT.CQRS.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PaymentRequest
{
    public class GetPaymentStatusListQuery : IQuery<List<PaymentStatusDto>>
    {
        [QueryLog]
        internal sealed class GetPaymentStatusListQueryHandler : IQueryHandler<GetPaymentStatusListQuery, List<PaymentStatusDto>>
        {
            private PaymentStatusRepository _paymentStatusRepository;
            public GetPaymentStatusListQueryHandler(PaymentStatusRepository paymentStatusRepository)
            {
                _paymentStatusRepository = paymentStatusRepository;
            }
            public List<PaymentStatusDto> Handle(GetPaymentStatusListQuery query)
            {
                return _paymentStatusRepository.GetAll();
            }
        }
    }
}
