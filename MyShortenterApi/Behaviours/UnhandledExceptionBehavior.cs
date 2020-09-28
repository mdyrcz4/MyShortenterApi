using MediatR;
using Microsoft.Extensions.Logging;
using MyShortenterApi.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyShortenterApi.Behaviours
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : CQRSResponseDto, new()
    {
        private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

        public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Request} - Unhandled exception caught.", typeof(TRequest).Name);
                var response = new TResponse();
                response.ServerError();
                return response;
            }
        }
    }
}