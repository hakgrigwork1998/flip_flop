using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_processor.Models;

namespace flip_flop_processor.Factories
{
    public class FlipProcessorResponseFactory : IFlipProcessorResponseFactory
    {
        public IFlipProcessorResponse Create(decimal winAmount, decimal balance, IErrorResponse errorResponse)
        {
            return new FlipProcessorResponse
            {
                Balance = balance,
                ErrorResponse = errorResponse,
                WinAmount = winAmount
            };
        }
    }
}
