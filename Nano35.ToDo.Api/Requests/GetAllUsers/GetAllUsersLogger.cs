using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Api.Requests.GetAllUsers
{
    public class GetAllUsersLogger :
        IPipelineNode<IGetAllUsersContract.IGetAllUsersRequestContract, IGetAllUsersContract.IGetAllUsersResultContract>
    {
        private readonly ILogger<GetAllUsersLogger> _logger;
        
        public class GetAllUsersLoggerError : 
            IGetAllUsersContract.IGetAllUsersErrorResultContract
        {
            public string Message { get; set; }
        }

        private readonly GetAllUsersCacher _validator;

        public GetAllUsersLogger(ILogger<GetAllUsersLogger> logger, GetAllUsersCacher validator)
        {
            _validator = validator;
            _logger = logger;
        }
        
        public async Task<IGetAllUsersContract.IGetAllUsersResultContract> Ask(
            IGetAllUsersContract.IGetAllUsersRequestContract request)
        {
            _logger.LogInformation($"Starts on: {DateTime.Now}");
            var result = await _validator.Ask(request);
            switch (result)
            {
                case IGetAllUsersContract.IGetAllUsersErrorResultContract error:
                    _logger.LogInformation($"Ends on: {DateTime.Now} with error: {error.Message}");
                    break;
                case IGetAllUsersContract.IGetAllUsersSuccessResultContract:
                    _logger.LogInformation($"Ends on: {DateTime.Now}");
                    break;
            }

            return result;
        }
    }
}