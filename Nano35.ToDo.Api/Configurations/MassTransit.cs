using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.ToDo.Common;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Api.Configurations
{
    public class MassTransitConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            var dest = "rabbitmq://192.168.100.120";
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
                }));
                x.AddRequestClient<ICreateUserContract.ICreateUserRequestContract>(
                    new Uri($"{dest}/ICreateUserRequestContract"));
                x.AddRequestClient<IGetAllUsersContract.IGetAllUsersRequestContract>(
                    new Uri($"{dest}/IGetAllUsersRequestContract"));
                x.AddRequestClient<IGetUserByIdContract.IGetAllUserByIdRequestContract>(
                    new Uri($"{dest}/IGetAllUserByIdRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}