using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Models.Casino.Response
{
    public class GetBalanceResponse : IGetBalanceResponse
    {
        [JsonProperty("TotalCoin")]
        public string TotalCoin { get; set; }

        [JsonProperty("TotalBalance")]
        public decimal? TotalBalance { get; set; }

        [JsonProperty("CurrencyId")]
        public string CurrencyId { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("HasError")]
        public bool HasError { get; set; }

        [JsonProperty("ErrorId")]
        public int ErrorId { get; set; }

        [JsonProperty("ErrorDescription")]
        public string ErrorDescription { get; set; }

        [JsonProperty("ErrorValue")]
        public object ErrorValue { get; set; }
    }
}
