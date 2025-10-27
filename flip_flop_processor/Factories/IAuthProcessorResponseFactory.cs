using flip_flop_processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Factories
{
    public interface IAuthProcessorResponseFactory
    {
        IAuthProcessorResponse Create(decimal balance, IErrorResponse errorResponse);
    }
}
