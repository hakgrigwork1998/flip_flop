using flip_flop_dal.Repositories;
using flip_flop_dal.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace flip_flop_dal.ServiceCollectionConfig
{
    public static class RegisterDataAccessLayerTypes
    {
        public static void RegisterDataAccessLayer(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            serviceCollection.AddScoped<IPlayerRepository, PlayerRepository>();
            serviceCollection.AddScoped<ITransactionRepository, TransactionRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            //TODO:Needs to be reviewed
            serviceCollection.AddDbContext<FlipFlopContext>(opts => opts.UseSqlServer(configuration["ConnectionString:FlipFlopDB"], x => x.MigrationsAssembly("flip_flop_dal")),ServiceLifetime.Scoped);
        }
    }
}
