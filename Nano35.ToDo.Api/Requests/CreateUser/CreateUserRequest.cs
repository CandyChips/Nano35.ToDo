using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Api.Requests.CreateUser
{
    public class CreateUserRequest :
        IPipelineNode<ICreateUserContract.ICreateUserRequestContract, ICreateUserContract.ICreateUserResultContract>
    {
        private readonly IBus _bus;

        public CreateUserRequest(IBus bus)
        {
            _bus = bus;
        }

        public async Task<ICreateUserContract.ICreateUserResultContract> Ask(
            ICreateUserContract.ICreateUserRequestContract request)
        {
            
            var client = _bus.CreateRequestClient<ICreateUserContract.ICreateUserRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateUserContract.ICreateUserSuccessResultContract, ICreateUserContract.ICreateUserErrorResultContract>(request);
            
            if (response.Is(out Response<ICreateUserContract.ICreateUserSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateUserContract.ICreateUserErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}