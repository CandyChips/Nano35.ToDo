using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.ToDo.Artifacts;
using Nano35.ToDo.Processor.Models;
using Nano35.ToDo.Processor.Services;

namespace Nano35.ToDo.Processor.UseCases.AddMessage
{
    public class AddMessage : UseCaseEndPointNodeBase<IAddMessageRequestContract, IAddMessageResultContract>
    {
        private readonly ApplicationContext _context;
        public AddMessage(ApplicationContext context) { _context = context; }
        public override async Task<UseCaseResponse<IAddMessageResultContract>> Ask(
            IAddMessageRequestContract input,
            CancellationToken cancellationToken)
        {
            var entity =
                new Message()
                    {Date = DateTime.Now, 
                     Id = input.NewId, 
                     Text = input.Text,
                     InstanceId = input.InstanceId,
                     FromUserId = input.FromUserId, 
                     ToUserId = input.ToUserId};
            await _context.Messages.AddAsync(entity, cancellationToken);
            return Pass(new AddMessageResultContract());
        }
    }
}