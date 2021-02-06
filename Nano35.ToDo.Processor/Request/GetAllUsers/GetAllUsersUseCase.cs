using System;
using System.Collections.Generic;
using System.Linq;
using Nano35.ToDo.Common;
using Nano35.ToDo.Processor.Contexts;
using Nano35.ToDo.Processor.Models.User;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Processor.Request.GetAllUsers
{
    public class GetAllUsersUseCase :
        ICommandRequest<IGetAllUsersContract.IGetAllUsersResultContract>
    {
        private class GetAllUsersSuccessResult : 
            IGetAllUsersContract.IGetAllUsersSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class UserViewModel : IUserViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        private readonly IApplicationContext _context;

        public GetAllUsersUseCase(IApplicationContext context)
        {
            _context = context;
        }

        public IGetAllUsersContract.IGetAllUsersResultContract Ask(
            IGetAllUsersContract.IGetAllUsersRequestContract request)
        {
            return new GetAllUsersSuccessResult() { Data = _context.Users.Select(a => new UserViewModel { Id = a.Id, Name = a.Name} ).ToList()};
        }
    }
}