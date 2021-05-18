using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.ToDo.Artifacts;
using Nano35.Contracts.ToDo.Models;
using Nano35.ToDo.Processor.Services;

namespace Nano35.ToDo.Processor.UseCases.GetAllMessages
{
    public class GetAllMessages : UseCaseEndPointNodeBase<IGetAllMessagesRequestContract, IGetAllMessagesResultContract>
    {
        private record MessageDto(Guid Id, DateTime Date, string Text, Guid InstanceId, Guid FromId, Guid ToId);
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public GetAllMessages(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllMessagesResultContract>> Ask(
            IGetAllMessagesRequestContract input,
            CancellationToken cancellationToken)
        {
            var messages = _context
                .Messages
                .Select(a => new MessageDto(a.Id, a.Date, a.Text, a.InstanceId, a.FromUserId, a.ToUserId))
                .ToList();
            var result = new List<Message>();
            foreach (var item in messages)
            {
                var res = new Message() { Id = item.Id, Date = item.Date, Text = item.Text };
                var getFromUserStringByIdRequestContract = new MasstransitUseCaseRequest<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>(_bus, new GetWorkerStringByIdRequestContract() {WorkerId = item.FromId, InstanceId = item.InstanceId}).GetResponse().Result;
                if (getFromUserStringByIdRequestContract.IsSuccess()) res.From = getFromUserStringByIdRequestContract.Success.Data;
                else return Pass(getFromUserStringByIdRequestContract.Error);

                var getToUserStringByIdRequestContract = new MasstransitUseCaseRequest<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>(_bus, new GetWorkerStringByIdRequestContract() {WorkerId = item.ToId, InstanceId = item.InstanceId}).GetResponse().Result;
                if (getToUserStringByIdRequestContract.IsSuccess()) res.To = getToUserStringByIdRequestContract.Success.Data;
                else return Pass(getToUserStringByIdRequestContract.Error);

                var getInstanceStringByIdRequestContract = new MasstransitUseCaseRequest<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract>(_bus, new GetInstanceStringByIdRequestContract() {InstanceId = item.InstanceId}).GetResponse().Result;
                if (getToUserStringByIdRequestContract.IsSuccess()) res.To += $" (+7{getToUserStringByIdRequestContract.Success.Data})";
                else return Pass(getToUserStringByIdRequestContract.Error);
                
                result.Add(res);
            }
            return Pass(new GetAllMessagesResultContract() { Messages = result });
        }
    }
}