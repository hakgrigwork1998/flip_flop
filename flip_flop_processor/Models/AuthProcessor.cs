using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public class AuthProcessor : IAuthProcessor
    {
        public int GameId { get; set; }

        public string Token { get; set; }

        public string Currency { get; set; }
    }
}
