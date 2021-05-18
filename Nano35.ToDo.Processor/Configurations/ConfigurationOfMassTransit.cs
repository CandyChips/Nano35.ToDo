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
                x.AddRequestClient<IGetClientByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetClientByIdRequestContract"));
                x.AddRequestClient<IGetUnitByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUnitByIdRequestContract"));
                x.AddRequestClient<IGetUnitStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetClientStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
                x.AddRequestClient<ICreateComingCashOperationRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateComingCashOperationRequestContract"));
                x.AddRequestClient<IGetImagesOfStorageItemRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetImagesOfStorageItemRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}