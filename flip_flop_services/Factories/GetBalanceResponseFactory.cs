using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_services.Models.Casino.Response;

namespace flip_flop_services.Factories
{
    public class GetBalanceResponseFactory : IGetBalanceResponseFactory
    {
        public IGetBalanceResponse Create(string totalCoin, decimal? totalBalance, string currencyId, string token, bool hasError, int errorId, string errorDescription, object errorValue)
        {
            return new GetBalanceResponse
            {
                TotalCoin = totalCoin,
                TotalBalance = totalBalance,
                CurrencyId = currencyId,
                Token = token,
                HasError = hasError,
                ErrorId = errorId,
                ErrorDescription = errorDescription,
                ErrorValue = errorValue
            };
        }
    }
}
