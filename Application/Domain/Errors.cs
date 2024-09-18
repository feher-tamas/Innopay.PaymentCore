using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public static class Errors
    {
        public static class Payment
        {
            public static Error AmountCannotBeLessThenZero(decimal amount) =>
                new Error("payment.amount.cannot.be.less.than.zero", $"Payment amount '{amount}' cannot be less than zero");

            /* other errors specific to students go here */
        }

        public static class General
        {
            public static Error NotFound(string entityName, Guid id) =>
                new Error("record.not.found", $"'{entityName}' not found for Id '{id}'");

            /* other general errors go here */
        }
    }
}
