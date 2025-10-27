using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Models.Casino.Response
{
    public interface IDoRollebackResponse
    {
        string TransactionId { get; }
        decimal? Balance { get; }
        string CurrencyId { get; }
        string Token { get; }
        bool HasError { get; }
        int ErrorId { get; }
        string ErrorDescription { get; }
        object ErrorValue { get; }
    }
}
