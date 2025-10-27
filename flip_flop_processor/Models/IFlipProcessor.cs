using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public interface IFlipProcessor
    {
        int PlayerId { get; }
        int GameId { get; }
        string Token { get; }
        string Currency { get; }
        Guid TransactionId { get; }
        decimal BetAmount { get; }
    }
}
