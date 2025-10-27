using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public class FlipProcessor : IFlipProcessor
    {
        public int PlayerId { get; set; }

        public int GameId { get; set; }

        public string Token { get; set; }

        public string Currency { get; set; }

        public decimal BetAmount { get; set; }

        public Guid TransactionId { get; set; }
    }
}
