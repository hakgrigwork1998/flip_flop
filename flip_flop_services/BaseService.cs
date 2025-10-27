using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace flip_flop_services
{
    public abstract class BaseService
    {
        protected readonly IConfiguration _configuration;
        protected readonly HttpClient _httpClient;

        public BaseService(IConfiguration configuration,HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
    }
}
