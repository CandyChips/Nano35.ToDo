using System.Threading.Tasks;
using MassTransit;
using Nano35.ToDo.Processor.Contexts;
using Nano35.ToDo.Processor.Request.CreateUser;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.GetAllUsers
{
    public class GetAllUsersConsumer :
        IConsumer<IGetAllUsersContract.IGetAllUsersRequestContract>
    {
        private readonly IApplicationContext _context;

        public GetAllUsersConsumer(IApplicationContext context)
        {
            _context = context;
        }

        public async Task Consume(
            ConsumeContext<IGetAllUsersContract.IGetAllUsersRequestContract> context)
        {
            var message = context.Message;

            var result =
                new GetAllUsersLogger(new GetAllUsersUseCase(_context)).Ask(message);
            
            switch (result)
            {
                case IGetAllUsersContract.IGetAllUsersSuccessResultContract:
                    await context.RespondAsync<IGetAllUsersContract.IGetAllUsersSuccessResultContract>(result);
                    break;
                case IGetAllUsersContract.IGetAllUsersErrorResultContract:
                    await context.RespondAsync<IGetAllUsersContract.IGetAllUsersErrorResultContract>(result);
                    break;
            }
            
        }
    }
}