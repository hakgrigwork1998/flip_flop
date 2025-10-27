using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_services.Models.Casino.Response;

namespace flip_flop_services.Factories
{
    public class DoRollebackResponseFactory : IDoRollebackResponseFactory
    {
        public IDoRollebackResponse Create(string transactionId, decimal? balance, string currencyId, string token, bool hasError, int errorId, string errorDescription, object errorValue)
        {
            return new DoRollebackResponse
            {
                TransactionId = transactionId,
                Balance = balance,
                CurrencyId = currencyId,
                ErrorDescription = errorDescription,
                ErrorId = errorId,
                ErrorValue = errorValue,
                HasError = hasError,
                Token = token
            };
        }
    }
}
