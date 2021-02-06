using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nano35.ToDo.Api.Requests.CreateUser;
using Nano35.ToDo.Api.Requests.GetAllUsers;
using Nano35.ToDo.RequestContracts;

namespace Nano35.ToDo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<CreateUserLogger> _loggerOfCreateUser;
        private readonly ILogger<GetAllUsersLogger> _loggerOfGetAllUsers;
        private readonly IBus _bus;

        public WeatherForecastController( IBus bus, ILogger<GetAllUsersLogger> loggerOfGetAllUsers, ILogger<CreateUserLogger> loggerOfCreateUser, IDistributedCache distributedCache)
        {
            _bus = bus;
            _loggerOfGetAllUsers = loggerOfGetAllUsers;
            _loggerOfCreateUser = loggerOfCreateUser;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public class CreateUserRequestBody :
            ICreateUserContract.ICreateUserRequestContract
        {
            public Guid NewId { get; set; }
            public string Name { get; set; }
        }
        
        [HttpPost]
        [Route("CreaateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestBody request)
        {
            var result = 
                await new CreateUserLogger(_loggerOfCreateUser,
                    new CreateUserValidator(
                        new CreateUserRequest(_bus))
                    ).Ask(request);
            
            return result switch
            {
                ICreateUserContract.ICreateUserSuccessResultContract success => Ok(),
                ICreateUserContract.ICreateUserErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
            
        }

        public class GetAllUsersRequestBody :
            IGetAllUsersContract.IGetAllUsersRequestContract
        {
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromRoute] GetAllUsersRequestBody request)
        {
            var result =
                await new GetAllUsersLogger(_loggerOfGetAllUsers, new GetAllUsersCacher( _distributedCache, new GetAllUsersRequest(_bus))).Ask(request);
            
            return result switch
            {
                IGetAllUsersContract.IGetAllUsersSuccessResultContract success => Ok(success.Data),
                IGetAllUsersContract.IGetAllUsersErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}
