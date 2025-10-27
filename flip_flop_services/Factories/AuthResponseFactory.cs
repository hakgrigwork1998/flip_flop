using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_services.Casino.Response;

namespace flip_flop_services.Factories
{
    public class AuthResponseFactory : IAuthResponseFactory
    {
        public IAuthResponse Create(string currency, int? playerId, long? externalId, int? partnerId, string userName, bool? isVerified, string token, bool hasError, int errorId, string errorDescription, object errorValue)
        {
            return new AuthResponse
            {
                Currency = currency,
                ErrorDescription = errorDescription,
                ErrorId = errorId,
                ErrorValue = errorValue,
                ExternalId = externalId,
                HasError = hasError,
                IsVerified = isVerified,
                PartnerId = partnerId,
                PlayerId = playerId,
                Token = token,
                UserName = userName
            };
        }
    }
}
