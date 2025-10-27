using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_processor.Models;

namespace flip_flop_processor.Factories
{
    public class FlipProcessorFactory : IFlipProcessorFactory
    {
        public IFlipProcessor Create(int playerId, int gameId, string token, string currency, Guid transactionId, decimal betAmount)
        {
            return new FlipProcessor
            {
                BetAmount = betAmount,
                Currency = currency,
                GameId = gameId,
                PlayerId = playerId,
                Token = token,
                TransactionId = transactionId
            };
        }
    }
}
