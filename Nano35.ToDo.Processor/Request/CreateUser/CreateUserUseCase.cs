using System;
using Nano35.ToDo.Common;
using Nano35.ToDo.Processor.Contexts;
using Nano35.ToDo.Processor.Models.User;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.CreateUser
{
    public class CreateUserUseCase :
        ICommandRequest<ICreateUserContract.ICreateUserResultContract>
    {
        public class CreateUserSuccessResult : 
            ICreateUserContract.ICreateUserSuccessResultContract
        {
            
        }

        private readonly IApplicationContext _context;

        public CreateUserUseCase(IApplicationContext context)
        {
            _context = context;
        }

        public ICreateUserContract.ICreateUserResultContract Ask(
            ICreateUserContract.ICreateUserRequestContract request)
        {
            _context.Users.Add(new User()
            {
                Id = request.NewId, 
                Name = request.Name,
                Role = null, 
                Status = null,
                IsDeleted = false,
                RoleId = Guid.Empty,
                StatusId = Guid.Empty
            });
            return new CreateUserSuccessResult() {};
        }
    }
}