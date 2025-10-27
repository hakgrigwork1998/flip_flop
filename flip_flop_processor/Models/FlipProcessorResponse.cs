using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public class FlipProcessorResponse : IFlipProcessorResponse
    {
        public decimal WinAmount { get; set; }

        public decimal Balance { get; set; }

        public IErrorResponse ErrorResponse { get; set; }
    }
}
