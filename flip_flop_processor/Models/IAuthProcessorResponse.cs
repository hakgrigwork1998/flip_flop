using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public interface IAuthProcessorResponse
    {
        decimal Balance { get; }
        IErrorResponse ErrorResponse { get; }
    }
}
