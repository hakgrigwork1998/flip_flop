using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flip_flop_services.Casino.Response
{
    public interface IAuthResponse
    {
        string Currency { get;}
        int? PlayerId { get;}
        long? ExternalId { get;}
        int? PartnerId { get;}
        string UserName { get;}
        bool? IsVerified { get;}
        string Token { get;}
        bool HasError { get;}
        int ErrorId { get;}
        string ErrorDescription { get;}
        object ErrorValue { get;}
    }
}
