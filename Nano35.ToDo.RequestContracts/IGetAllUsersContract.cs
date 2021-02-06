using System;
using System.Collections.Generic;

namespace Nano35.ToDo.RequestContracts
{
    public interface IGetAllUsersContract
    {
        public interface IGetAllUsersRequestContract:
            IRequestContract
        {
               
        }
        
        public interface IGetAllUsersResultContract :
            IResultContract
        {
               
        }
        
        public interface IGetAllUsersSuccessResultContract :
            IGetAllUsersResultContract,
            ISuccessRequestResult
        {
               IEnumerable<IUserViewModel> Data { get; set; }
        }
        
        public interface IGetAllUsersErrorResultContract :
            IGetAllUsersResultContract,
            IErrorRequestResult
        {
            string Message { get; set; }
        }
    }

    public interface IUserViewModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}