using System;
using Nano35.ToDo.Common;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.CreateUser
{
    public class CreateUserLogger :
        ICommandRequest<ICreateUserContract.ICreateUserResultContract>
    {
        public class CreateUserLoggerError : 
            ICreateUserContract.ICreateUserErrorResultContract
        {
            public string Message { get; set; }
        }

        private readonly CreateUserValidator _validator;

        public CreateUserLogger(CreateUserValidator validator)
        {
            _validator = validator;
        }
        
        public ICreateUserContract.ICreateUserResultContract Ask(
            ICreateUserContract.ICreateUserRequestContract request)
        {
            return _validator.Ask(request);
        }
    }
}