using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public class ErrorResponse : IErrorResponse
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }
}
