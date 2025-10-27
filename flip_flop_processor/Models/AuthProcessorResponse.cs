using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public class AuthProcessorResponse : IAuthProcessorResponse
    {
        public decimal Balance { get; set; }

        public IErrorResponse ErrorResponse { get; set; }
    }
}
