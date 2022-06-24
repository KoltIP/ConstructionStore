using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared
{
    public static class SystemConfiguration
    {
        private static IConfigurationRoot configurationRoot;
        public static IConfigurationRoot Configuration
        {
            get
            {
                if (configurationRoot == null)
                {
                    configurationRoot = BuildConfiguration();
                }
                return configurationRoot;
            }
        }
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
