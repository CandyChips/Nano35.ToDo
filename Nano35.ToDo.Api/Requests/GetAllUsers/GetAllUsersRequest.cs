using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Api.Requests.GetAllUsers
{
    public class GetAllUsersRequest :
        IPipelineNode<IGetAllUsersContract.IGetAllUsersRequestContract, IGetAllUsersContract.IGetAllUsersResultContract>
    {
        private readonly IBus _bus;

        public GetAllUsersRequest(IBus bus)
        {
            _bus = bus;
        }

        public async Task<IGetAllUsersContract.IGetAllUsersResultContract> Ask(
            IGetAllUsersContract.IGetAllUsersRequestContract request)
        {
            
            var client = _bus.CreateRequestClient<IGetAllUsersContract.IGetAllUsersRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllUsersContract.IGetAllUsersSuccessResultContract, IGetAllUsersContract.IGetAllUsersErrorResultContract>(request);
            
            if (response.Is(out Response<IGetAllUsersContract.IGetAllUsersSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllUsersContract.IGetAllUsersErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}