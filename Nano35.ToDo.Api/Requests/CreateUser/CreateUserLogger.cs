using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.ToDo.Common;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Api.Requests.CreateUser
{
    public class CreateUserLogger :
        IPipelineNode<ICreateUserContract.ICreateUserRequestContract, ICreateUserContract.ICreateUserResultContract>
    {
        private readonly ILogger<CreateUserLogger> _logger;
        
        public class CreateUserLoggerError : 
            ICreateUserContract.ICreateUserErrorResultContract
        {
            public string Message { get; set; }
        }

        private readonly CreateUserValidator _validator;

        public CreateUserLogger(ILogger<CreateUserLogger> logger, CreateUserValidator validator)
        {
            _validator = validator;
            _logger = logger;
        }
        
        public async Task<ICreateUserContract.ICreateUserResultContract> Ask(
            ICreateUserContract.ICreateUserRequestContract request)
        {
            _logger.LogInformation($"Starts on: {DateTime.Now}");
            var result = await _validator.Ask(request);
            switch (result)
            {
                case ICreateUserContract.ICreateUserErrorResultContract error:
                    _logger.LogInformation($"Ends on: {DateTime.Now} with error: {error.Message}");
                    break;
                case ICreateUserContract.ICreateUserSuccessResultContract:
                    _logger.LogInformation($"Ends on: {DateTime.Now}");
                    break;
            }

            return result;
        }
    }
}