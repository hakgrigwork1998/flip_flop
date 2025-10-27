using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace flip_flop_test
{
    public static class SingletonConfiguration
    {
        private static IConfiguration instance = null;
        private static readonly object padlock = new object();

        public static IConfiguration Configuration
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json");

                            instance = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", true, true)
                                .Build();
                        }
                    }
                }
                return instance;
            }
        }

    }
}
