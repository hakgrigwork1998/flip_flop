using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Models.Casino.Request
{
    public class DoRollebackRequest
    {
        public string TransactionId { get; set; }
        public int GameId { get; set; }
        public string Token { get; set; }
        public int PlayerId { get; set; }
        public string CurrencyId { get; set; }

        public DoRollebackRequest(string transactionId,int gameId,string token,int playerId,string currencyId)
        {
            this.TransactionId = transactionId;
            this.GameId = gameId;
            this.Token = token;
            this.PlayerId = playerId;
            this.CurrencyId = currencyId;
        }
    }
}
