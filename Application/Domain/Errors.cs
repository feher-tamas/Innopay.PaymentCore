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
            public static Error AmountIsTaken(decimal amount) =>
                new Error("payment.amount.is.taken", $"Payment amount '{amount}' is taken");

            /* other errors specific to students go here */
        }

        public static class General
        {
            public static Error NotFound(string entityName, long id) =>
                new Error("record.not.found", $"'{entityName}' not found for Id '{id}'");

            /* other general errors go here */
        }
    }
}
