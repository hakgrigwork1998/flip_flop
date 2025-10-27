using flip_flop_processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Factories
{
    public interface IFlipProcessorResponseFactory
    {
        IFlipProcessorResponse Create(decimal winAmount, decimal balance, IErrorResponse errorResponse);
    }
}
