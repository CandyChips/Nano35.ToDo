using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.ToDo.Artifacts;

namespace Nano35.ToDo.Api.Configurations
{
    public class MassTransitConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {   
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri($"{ContractBase.RabbitMqLocation}/"), h =>
                    {
                        h.Username(ContractBase.RabbitMqUsername);
                        h.Password(ContractBase.RabbitMqPassword);
                    });
                }));
                x.AddRequestClient<IAddMessageRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IAddMessageRequestContract"));
                x.AddRequestClient<IGetAllMessagesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllMessagesRequestContract"));
                
            });
            services.AddMassTransitHostedService();
        }
    }
}