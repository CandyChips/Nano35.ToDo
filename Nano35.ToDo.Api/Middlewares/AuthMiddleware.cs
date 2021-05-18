using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Nano35.Storage.Api.Middlewares
{
    public class UseAuthMiddleware
    {    
        private readonly RequestDelegate _next;
        private readonly ILogger<UseAuthMiddleware> _logger;
        public UseAuthMiddleware(RequestDelegate next, ILogger<UseAuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var token = context.Request.Headers["Authorization"]!.ToString().Split(' ').Last();
            if (token != "")
            {
                _logger.Log(LogLevel.Information, token);
            }
            await _next.Invoke(context);
        }
    }
}