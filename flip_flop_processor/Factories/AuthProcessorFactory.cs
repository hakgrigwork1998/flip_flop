using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_processor.Models;

namespace flip_flop_processor.Factories
{
    public class AuthProcessorFactory : IAuthProcessorFactory
    {
        public IAuthProcessor Create(int gameId, string token,string currency)
        { 
            return new AuthProcessor
            {
                GameId = gameId,
                Token = token,
                Currency=currency
            };
        }
    }
}
