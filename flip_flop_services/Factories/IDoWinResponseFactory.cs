using flip_flop_services.Models.Casino.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Factories
{
    public interface IDoWinResponseFactory
    {
        IDoWinResponse Create(string transactionId, decimal? balance, string currencyId, string token, bool hasError, int errorId, string errorDescription, object errorValue);
    }
}
