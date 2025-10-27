using flip_flop_processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Factories
{
    public interface IFlipProcessorFactory
    {
        IFlipProcessor Create(int playerId, int gameId, string token, string currency, Guid transactionId, decimal betAmount);
    }
}
