﻿using Microsoft.Extensions.Configuration;

namespace DotnetAngularMiniEcommerce_API.Persistence
{
    public static class Configuration
    {
        private static ConfigurationManager _configurationManager;

        private static ConfigurationManager ConfigurationManager {
            get {
                if (_configurationManager == null)
                {
                    _configurationManager = new ConfigurationManager();
                    _configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/DotnetAngularMiniEcommerce_API.API"));
                    _configurationManager.AddJsonFile("appsettings.json");
                }
                return _configurationManager;
            }
        }

        public static string ConnectionString {
            get {
                return ConfigurationManager.GetConnectionString("PostgreSQL");
            }
        }

        public static List<string> CorsUrlList
        {
            get
            {
                return ConfigurationManager.GetSection("CorsUrlList").Get<List<string>>();
            }
        }
    }
}
