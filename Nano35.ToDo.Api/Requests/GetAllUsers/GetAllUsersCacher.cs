using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nano35.ToDo.RequestContracts;
using Newtonsoft.Json;

namespace Nano35.ToDo.Api.Requests.GetAllUsers
{
    public class GetAllUsersCacher :
        IPipelineNode<IGetAllUsersContract.IGetAllUsersRequestContract, IGetAllUsersContract.IGetAllUsersResultContract>
    {
        private readonly IDistributedCache _distributedCache;
        
        public class GetAllUsersLoggerError : 
            IGetAllUsersContract.IGetAllUsersErrorResultContract
        {
            public string Message { get; set; }
        }

        private readonly GetAllUsersRequest _validator;

        public GetAllUsersCacher(IDistributedCache distributedCache, GetAllUsersRequest validator)
        {
            _validator = validator;
            this._distributedCache = distributedCache;
        }
        
        public class GetAllUsersSuccessResult : IGetAllUsersContract.IGetAllUsersSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }
        
        public class UserViewModel :  IUserViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public async Task<IGetAllUsersContract.IGetAllUsersResultContract> Ask(
            IGetAllUsersContract.IGetAllUsersRequestContract request)
        {
            GetAllUsersSuccessResult result = null;
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<UserViewModel>();
            var redisCustomerList = await _distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                result = new GetAllUsersSuccessResult(){Data =  JsonConvert.DeserializeObject<List<UserViewModel>>(serializedCustomerList)};
            }
            else
            {
                result = await _validator.Ask(request) as GetAllUsersSuccessResult;
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return result;
        }
    }
}