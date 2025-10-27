using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_processor.Models;

namespace flip_flop_processor.Factories
{
    public class AuthProcessorResponseFactory : IAuthProcessorResponseFactory
    {
        public IAuthProcessorResponse Create(decimal balance, IErrorResponse errorResponse)
        {
            return new AuthProcessorResponse
            {
                Balance = balance,
                ErrorResponse = errorResponse
            };
        }
    }
}
