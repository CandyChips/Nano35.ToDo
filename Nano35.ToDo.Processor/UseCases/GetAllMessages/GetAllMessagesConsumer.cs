using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.ToDo.Artifacts;
using Nano35.ToDo.Processor.Services;

namespace Nano35.ToDo.Processor.UseCases.GetAllMessages
{
    public class GetAllMessagesConsumer : IConsumer<IGetAllMessagesRequestContract>
    {
        private readonly IServiceProvider _services;
        public GetAllMessagesConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllMessagesRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllMessagesRequestContract, IGetAllMessagesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllMessagesRequestContract>)) as ILogger<IGetAllMessagesRequestContract>,
                    new GetAllMessages(_services.GetService(typeof(ApplicationContext)) as ApplicationContext, _services.GetService(typeof(IBus)) as IBus))
                .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}