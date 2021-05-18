using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.ToDo.Artifacts;
using Nano35.ToDo.Api.UseCases;
using Nano35.ToDo.Api.UseCases.AddMessage;
using Nano35.ToDo.Api.UseCases.GetAllMessages;

namespace Nano35.ToDo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly IServiceProvider _services;
        public Controller(IServiceProvider services) { _services = services; }
        
        [HttpGet("AllMessages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public async Task<IActionResult> GetAllMessages(Guid from, Guid to) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllMessagesRequestContract, IGetAllMessagesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllMessagesRequestContract>)) as ILogger<IGetAllMessagesRequestContract>,
                        new GetAllMessagesRequest(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllMessagesRequestContract()
                    {
                        From = from,
                        To = to
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public async Task<IActionResult> CreateRepairOrder(Guid from, Guid to, string message) 
        {
            var result =
                await new LoggedUseCasePipeNode<IAddMessageRequestContract, IAddMessageResultContract>(
                        _services.GetService(typeof(ILogger<IAddMessageRequestContract>)) as
                            ILogger<IAddMessageRequestContract>,
                        new AddMessageRequest(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new AddMessageRequestContract()
                    {
                        NewId = Guid.NewGuid(),
                        InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                        FromUserId = from,
                        ToUserId = to,
                        Text = message
                    });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}
