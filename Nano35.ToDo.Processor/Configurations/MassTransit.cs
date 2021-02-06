using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.ToDo.Common;
using Nano35.ToDo.Processor.Request.CreateUser;
using Nano35.ToDo.Processor.Request.GetAllUsers;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Configurations
{
    public class MassTransitConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            const string dest = "rabbitmq://192.168.100.120";
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri(dest), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    
                    cfg.ReceiveEndpoint("ICreateUserRequestContract", e =>
                    {
                        e.Consumer<CreateUserConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllUsersRequestContract", e =>
                    {
                        e.Consumer<GetAllUsersConsumer>(provider);
                    });
                    
                }));
                x.AddConsumer<CreateUserConsumer>();
                x.AddConsumer<GetAllUsersConsumer>();
                
            });
            services.AddMassTransitHostedService();
        }
    }
}