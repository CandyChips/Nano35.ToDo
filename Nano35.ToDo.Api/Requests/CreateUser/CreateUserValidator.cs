using System;
using System.Threading.Tasks;
using Nano35.ToDo.Common;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Api.Requests.CreateUser
{
    public class CreateUserValidator :
        IPipelineNode<ICreateUserContract.ICreateUserRequestContract, ICreateUserContract.ICreateUserResultContract>
    {
        public class CreateUserValidatorError : 
            ICreateUserContract.ICreateUserErrorResultContract
        {
            public string Message { get; set; }
        }
        
        private readonly CreateUserRequest _transaction;

        public CreateUserValidator(CreateUserRequest transaction)
        {
            _transaction = transaction;
        }
        
        public async Task<ICreateUserContract.ICreateUserResultContract> Ask(
            ICreateUserContract.ICreateUserRequestContract request)
        {
            if (new Random().Next() % 2 == 0)
            {
                return new CreateUserValidatorError() {Message = "Ошибка валидации со стороны клиента"};
            }
            else
            {
                return await _transaction.Ask(request);
            }
        }
    }
}