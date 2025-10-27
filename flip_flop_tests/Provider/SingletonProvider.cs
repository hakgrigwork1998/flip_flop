using flip_flop_tests;
using flip_flop_processor.ServiceCollectionConfig;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace flip_flop_core.Provider
{
    public static class SingletonProvider
    {
        private static IServiceProvider _serviceProvider;
        private static readonly object padlock = new object();

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    lock (padlock)
                    {
                        if (_serviceProvider == null)
                        {
                            var serviceCollection = new ServiceCollection();
                            serviceCollection.RegisterProcessor(SingletonConfiguration.Configuration);
                            _serviceProvider = serviceCollection.BuildServiceProvider();
                        }
                    }
                }
                return _serviceProvider;
            }
        }
    }
}
