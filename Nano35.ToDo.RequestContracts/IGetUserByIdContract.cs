namespace Nano35.ToDo.RequestContracts
{
    public interface IGetUserByIdContract
    {
        public interface IGetAllUserByIdRequestContract :
            IRequestContract
        {
               
        }
        public interface IGetAllUserByIdResultContract :
            IResultContract
        {
               
        }
        public interface IGetAllUserByIdSuccessResultContract :
            IGetAllUserByIdResultContract,
            ISuccessRequestResult
        {
               
        }
        public interface IGetAllUserByIdErrorResultContract :
            IGetAllUserByIdResultContract,
            IErrorRequestResult
        {
            string Message { get; set; }
        }
    }
}