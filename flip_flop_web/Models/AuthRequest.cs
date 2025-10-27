using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flip_flop_web.Models
{
    public class AuthRequest
    {
        [JsonProperty("game_id")]
        public int GameId { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("partner_id")]
        public int PartnerId { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
