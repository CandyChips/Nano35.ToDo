using System;
using Nano35.ToDo.Common;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.CreateUser
{
    public class CreateUserValidator :
        ICommandRequest<ICreateUserContract.ICreateUserResultContract>
    {
        public class CreateUserValidatorError : 
            ICreateUserContract.ICreateUserErrorResultContract
        {
            public string Message { get; set; }
        }
        
        private readonly CreateUserTransaction _transaction;

        public CreateUserValidator(CreateUserTransaction transaction)
        {
            _transaction = transaction;
        }
        
        public ICreateUserContract.ICreateUserResultContract Ask(
            ICreateUserContract.ICreateUserRequestContract request)
        {
            if (new Random().Next() % 2 == 0)
            {
                return new CreateUserValidatorError() {Message = "Ошибка валидации со стороны сервера"};
            }
            else
            {
                return _transaction.Ask(request);
            }
        }
    }
}