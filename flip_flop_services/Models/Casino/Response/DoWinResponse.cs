using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Models.Casino.Response
{
    public class DoWinResponse : IDoWinResponse
    {
        public string TransactionId { get; set; }

        public decimal? Balance { get; set; }

        public string CurrencyId { get; set; }

        public string Token { get; set; }

        public bool HasError { get; set; }

        public int ErrorId { get; set; }

        public string ErrorDescription { get; set; }

        public object ErrorValue { get; set; }
    }
}
