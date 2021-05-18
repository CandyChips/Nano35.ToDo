using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.ToDo.Artifacts;
using Nano35.ToDo.Processor.Services;

namespace Nano35.ToDo.Processor.UseCases.AddMessage
{
    public class AddMessageConsumer : IConsumer<IAddMessageRequestContract>
    {
        private readonly IServiceProvider _services;
        public AddMessageConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IAddMessageRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IAddMessageRequestContract, IAddMessageResultContract>(
                        _services.GetService(typeof(ILogger<IAddMessageRequestContract>)) as ILogger<IAddMessageRequestContract>,
                        new TransactedUseCasePipeNode<IAddMessageRequestContract, IAddMessageResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new AddMessage(_services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}