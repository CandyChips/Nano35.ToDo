using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.ToDo.Api.UseCases
{
    public class LoggedUseCasePipeNode<TIn, TOut> : UseCasePipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        private readonly ILogger<TIn> _logger;
        public LoggedUseCasePipeNode(ILogger<TIn> logger, IUseCasePipeNode<TIn, TOut> next) : base(next) => _logger = logger;
        public override async Task<UseCaseResponse<TOut>> Ask(TIn input)
        {
            try
            {
                var starts = DateTime.Now;
                var result = await DoNext(input);
                var time = DateTime.Now - starts;
                if (result.IsSuccess())
                    _logger.LogInformation($"Ends by: {time} with success.");
                else
                    _logger.LogWarning($"Ends by: {time} with error: {result.Error}.");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Ends by: {DateTime.Now} with exception: {e.Message}!!!");
                return new UseCaseResponse<TOut>($"ApiError");
            }
        }
    }
}