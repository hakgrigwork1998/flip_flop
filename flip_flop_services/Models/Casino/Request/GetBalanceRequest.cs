using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Models.Casino.Request
{
    public class GetBalanceRequest
    {
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("PlayerId")]
        public int PlayerId { get; set; }

        [JsonProperty("CurrencyId")]
        public string CurrencyId { get; set; }

        public GetBalanceRequest(string token,int playerId,string currencyId)
        {
            this.Token = token;
            this.PlayerId = playerId;
            this.CurrencyId = currencyId;
        }
    }
}
