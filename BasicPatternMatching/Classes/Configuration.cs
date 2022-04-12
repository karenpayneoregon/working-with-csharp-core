using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BasicPatternMatching.Classes
{
    public class Configuration
    {
        public static Settings ApplicationSettings()
        {
            var configuration = Builder();
            return configuration.GetSection("Settings").Get<Settings>();
        }

        private static IConfigurationRoot Builder()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration;
        }
    }
}
