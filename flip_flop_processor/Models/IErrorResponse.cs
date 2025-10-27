using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Models
{
    public interface IErrorResponse
    {
        int ErrorCode { get; }
        string Message { get; }
    }
}
