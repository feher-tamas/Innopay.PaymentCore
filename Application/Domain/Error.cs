using FunctionExtensions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public sealed class Error : ValueObject
    {
        public string Code { get; }
        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
