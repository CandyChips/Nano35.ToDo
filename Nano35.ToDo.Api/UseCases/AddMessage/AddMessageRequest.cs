using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.ToDo.Artifacts;

namespace Nano35.ToDo.Api.UseCases.AddMessage
{
    public class AddMessageRequest:
        UseCaseEndPointNodeBase<IAddMessageRequestContract, IAddMessageResultContract>
    {
        private readonly IBus _bus;
        public AddMessageRequest(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IAddMessageResultContract>> Ask(IAddMessageRequestContract input) =>
            await new MasstransitUseCaseRequest<IAddMessageRequestContract, IAddMessageResultContract>(_bus, input)
                .GetResponse();
    }   
}