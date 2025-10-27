using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_core.Enums;
using flip_flop_processor.Models;

namespace flip_flop_processor.Factories
{
    public class ErrorResponseFactory : IErrorResponseFactory
    {
        public IErrorResponse Create(int errorCode=(int)ErrorCode.Success, string message="Ok")
        {
            return new ErrorResponse
            {
                ErrorCode = errorCode,
                Message = message
            };
        }
    }
}
