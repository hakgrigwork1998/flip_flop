using flip_flop_dal.ServiceCollectionConfig;
using flip_flop_processor.Factories;
using flip_flop_processor.Processors;
using flip_flop_game_logic.ServiceCollectionConfig;
using flip_flop_services.ServiceCollectionConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace flip_flop_processor.ServiceCollectionConfig
{
    public static class RegisterProcessorTypes
    {
        public static void RegisterProcessor(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.RegisterService();
            serviceCollection.RegisterDataAccessLayer(configuration);
            serviceCollection.RegisterGameLogic();

            serviceCollection.AddTransient<IProcessor, Processor>();
            serviceCollection.AddScoped<IAuthProcessorFactory, AuthProcessorFactory>();
            serviceCollection.AddScoped<IAuthProcessorResponseFactory, AuthProcessorResponseFactory>();
            serviceCollection.AddScoped<IFlipProcessorFactory, FlipProcessorFactory>();
            serviceCollection.AddScoped<IFlipProcessorResponseFactory, FlipProcessorResponseFactory>();
            serviceCollection.AddScoped<IErrorResponseFactory, ErrorResponseFactory>();
        }
    }
}
