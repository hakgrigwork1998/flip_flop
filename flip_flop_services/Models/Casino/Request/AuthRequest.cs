using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flip_flop_services.Casino.Request
{
    public class AuthRequest
    {
        [JsonProperty("AuthenticationType")]
        public int AuthenticationType { get;} = 0;

        [JsonProperty("GameId")]
        public int GameId { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("CurrencyId")]
        public string Currency { get; set; }

        public AuthRequest(int gameId,string token,string currency)
        {
            this.GameId = gameId;
            this.Token = token;
            this.Currency = currency;
        }
    }
}
