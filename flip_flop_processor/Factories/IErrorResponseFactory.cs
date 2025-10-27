using flip_flop_core.Enums;
using flip_flop_processor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_processor.Factories
{
    public interface IErrorResponseFactory
    {
        IErrorResponse Create(int errorCode = (int)ErrorCode.Success, string message = "Ok");
    }
}
