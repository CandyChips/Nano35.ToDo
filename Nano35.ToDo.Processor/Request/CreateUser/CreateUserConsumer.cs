using System.Threading.Tasks;
using MassTransit;
using Nano35.ToDo.Processor.Contexts;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.CreateUser
{
    public class CreateUserConsumer :
        IConsumer<ICreateUserContract.ICreateUserRequestContract>
    {
        private readonly IApplicationContext _context;

        public CreateUserConsumer(IApplicationContext context)
        {
            _context = context;
        }

        public async Task Consume(
            ConsumeContext<ICreateUserContract.ICreateUserRequestContract> context)
        {
            var message = context.Message;

            var result = 
                new CreateUserLogger(
                    new CreateUserValidator(
                        new CreateUserTransaction(
                            new CreateUserUseCase(_context))))
                    .Ask(message);
            
            switch (result)
            {
                case ICreateUserContract.ICreateUserSuccessResultContract:
                    await context.RespondAsync<ICreateUserContract.ICreateUserSuccessResultContract>(result);
                    break;
                case ICreateUserContract.ICreateUserErrorResultContract:
                    await context.RespondAsync<ICreateUserContract.ICreateUserErrorResultContract>(result);
                    break;
            }
            
        }
    }
}