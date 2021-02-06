using System;
using Nano35.ToDo.Common;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.CreateUser
{
    public class CreateUserTransaction :
        ICommandRequest<ICreateUserContract.ICreateUserResultContract>
    {
        public class CreateUserTransactionError : 
            ICreateUserContract.ICreateUserErrorResultContract
        {
            public string Message { get; set; }
        }

        private readonly CreateUserUseCase _useCase;

        public CreateUserTransaction(CreateUserUseCase validator)
        {
            _useCase = validator;
        }
        
        public ICreateUserContract.ICreateUserResultContract Ask(
            ICreateUserContract.ICreateUserRequestContract request)
        {
            try
            {
                return _useCase.Ask(request);
            }
            catch (Exception e)
            {
                return new CreateUserTransactionError() {Message = "Повторите попытку позже"};
            }
        }
    }
}