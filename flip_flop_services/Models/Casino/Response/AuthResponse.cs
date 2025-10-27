using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flip_flop_services.Casino.Response
{ 
    public class AuthResponse :IAuthResponse
    {
        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("PlayerId")]
        public int? PlayerId { get; set; }

        [JsonProperty("UserExternalId")]
        public long? ExternalId { get; set; }

        [JsonProperty("PartnerId")]
        public int? PartnerId { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("IsVerified")]
        public bool? IsVerified { get; set; }

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
