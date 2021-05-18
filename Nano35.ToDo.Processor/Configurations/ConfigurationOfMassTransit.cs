using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Contracts.files;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.ToDo.Processor.UseCases.AddMessage;
using Nano35.ToDo.Processor.UseCases.GetAllMessages;

namespace Nano35.ToDo.Processor.Configurations
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
                    
                    cfg.ReceiveEndpoint("IAddMessageRequestContract", e => { e.Consumer<AddMessageConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllMessagesRequestContract", e => { e.Consumer<GetAllMessagesConsumer>(provider); });
                }));
                x.AddConsumer<AddMessageConsumer>();
                x.AddConsumer<GetAllMessagesConsumer>();
                
            });
            services.AddMassTransitHostedService();
        }
    }
}