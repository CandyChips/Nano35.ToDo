using Nano35.ToDo.Common;
using Nano35.ToDo.Processor.Request.CreateUser;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.GetAllUsers
{
    public class GetAllUsersLogger :
        ICommandRequest<IGetAllUsersContract.IGetAllUsersResultContract>
    {
        public class CreateUserLoggerError : 
            IGetAllUsersContract.IGetAllUsersErrorResultContract
        {
            public string Message { get; set; }
        }

        private readonly GetAllUsersUseCase _validator;

        public GetAllUsersLogger(GetAllUsersUseCase validator)
        {
            _validator = validator;
        }
        
        public IGetAllUsersContract.IGetAllUsersResultContract Ask(
            IGetAllUsersContract.IGetAllUsersRequestContract request)
        {
            return _validator.Ask(request);
        }
    }
}