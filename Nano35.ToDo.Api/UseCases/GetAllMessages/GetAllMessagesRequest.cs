using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.ToDo.Artifacts;

namespace Nano35.ToDo.Api.UseCases.GetAllMessages
{
    public class GetAllMessagesRequest:
        UseCaseEndPointNodeBase<IGetAllMessagesRequestContract, IGetAllMessagesResultContract>
    {
        private readonly IBus _bus;
        public GetAllMessagesRequest(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllMessagesResultContract>> Ask(IGetAllMessagesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllMessagesRequestContract, IGetAllMessagesResultContract>(_bus, input)
                .GetResponse();
    }   
}