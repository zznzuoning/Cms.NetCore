using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Infrastructure.Comm
{
    public class ConfigManager
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigManager()
        {

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false,true)
                .Build();

        }

    }


}
