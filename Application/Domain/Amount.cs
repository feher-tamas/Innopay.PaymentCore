using FunctionExtensions.Result;
using FunctionExtensions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Domain
{
    public class Amount : ValueObject
    {
        private readonly decimal _value;
        public decimal Value
        {
            get { return _value; }
        }
        private Amount(decimal value)
        {
            _value = value;
        }
        public static Result<Amount,Error> Create(decimal value)
        {
            if (value < 0)
            {
                return Result.Failure<Amount,Error>(Errors.Payment.AmountIsTaken(value));
            }
            return Result.Success<Amount,Error>(new Amount(value));
        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
        public static implicit operator decimal(Amount amount)
        {
            return amount.Value;
        }
    }
}
