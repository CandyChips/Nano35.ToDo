using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nano35.Contracts;
using Nano35.ToDo.Processor.Configurations;

namespace Nano35.ToDo.Processor
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("ServicesConfig.json");
            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {            
            new Configurator(services, new EntityFrameworkConfiguration(Configuration)).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
    }
}
