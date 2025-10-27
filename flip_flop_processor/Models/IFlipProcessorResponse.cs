using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public interface IFlipProcessorResponse
    {
        decimal WinAmount { get; }
        decimal Balance { get; }
        IErrorResponse ErrorResponse { get; }
    }
}
