using flip_flop_game_logic.Factories;
using flip_flop_game_logic.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace flip_flop_game_logic.ServiceCollectionConfig
{
    public static class RegisterGameLogicTypes
    {
        public static void RegisterGameLogic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGameCalculator, GameCalculator>();

            serviceCollection.AddScoped<IFlipDataResponseFactory, FlipDataResponseFactory>();
        }
    }
}
