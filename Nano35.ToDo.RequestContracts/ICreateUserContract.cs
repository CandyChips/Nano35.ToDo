using System;

namespace Nano35.ToDo.RequestContracts
{
    public interface ICreateUserContract
    {
        public interface ICreateUserRequestContract :
            IRequestContract
        {
            Guid NewId { get; set; }
            string Name { get; set; }
        }
        
        public interface ICreateUserResultContract :
            IResultContract
        {
               
        }
        
        public interface ICreateUserSuccessResultContract :
            ICreateUserResultContract,
            ISuccessRequestResult
        {
               
        }
        
        public interface ICreateUserErrorResultContract :
            ICreateUserResultContract,
            IErrorRequestResult
        {
            string Message { get; set; }
        }
    }
}
