using flip_flop_services.Casino.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.Factories
{
    public interface IAuthResponseFactory
    {
        IAuthResponse Create(string currency, int? playerId, long? externalId, int? partnerId, string userName, bool? isVerified, string token, bool hasError, int errorId, string errorDescription, object errorValue);
    }
}
