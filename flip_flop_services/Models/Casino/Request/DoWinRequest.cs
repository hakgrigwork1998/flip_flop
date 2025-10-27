using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Models.Casino.Request
{
    public class DoWinRequest
    {
        [JsonProperty("WinAmount")]
        public decimal WinAmount { get; set; }
        [JsonProperty("TransactionId")]
        public string TransactionId { get; set; }
        [JsonProperty("GameId")]
        public int GameId { get; set; }
        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("PlayerId")]
        public int PlayerId { get; set; }
        [JsonProperty("CurrencyId")]
        public string CurrencyId { get; set; }

        public DoWinRequest(decimal winAmount, string transactionId, int gameId, string token, int playerId, string currencyId)
        {
            this.WinAmount = winAmount;
            this.TransactionId = transactionId;
            this.GameId = gameId;
            this.Token = token;
            this.PlayerId = playerId;
            this.CurrencyId = currencyId;
        }
    }
}
