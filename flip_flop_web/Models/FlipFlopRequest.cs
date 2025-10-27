using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flip_flop_web.Models
{
    public class FlipFlopRequest
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }
        [JsonProperty("game_id")]
        public int GameId { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("bet_amount")]
        public decimal BetAmount { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
