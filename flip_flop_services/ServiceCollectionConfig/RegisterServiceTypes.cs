using flip_flop_services.Casino;
using flip_flop_services.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace flip_flop_services.ServiceCollectionConfig
{
    public static class RegisterServiceTypes
    {
        public static void RegisterService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<ICasinoService, CasinoService>();

            serviceCollection.AddScoped<IAuthResponseFactory, AuthResponseFactory>();
            serviceCollection.AddScoped<IDoBetResponseFactory, DoBetResponseFactory>();
            serviceCollection.AddScoped<IDoWinResponseFactory, DoWinResponseFactory>();
            serviceCollection.AddScoped<IGetBalanceResponseFactory, GetBalanceResponseFactory>();
            serviceCollection.AddScoped<IDoRollebackResponseFactory, DoRollebackResponseFactory>();
        }
    }
}
