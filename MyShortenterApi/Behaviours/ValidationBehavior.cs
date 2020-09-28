using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyShortenterApi.Dtos;
using MyShortenterApi.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyShortenterApi.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : CQRSResponseDto, new()
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();
            if (validator != null)
            {
                var requestName = typeof(TRequest).Name;
                var result = await validator.Validate(request);
                if (result.IsSuccess == false)
                {
                    _logger.LogWarning("{Request} - Validation failed. Reason: {Reason}", requestName, result.ErrorMessage);
                    var response = new TResponse();
                    response.BadRequest(result);
                    return response;
                }
                _logger.LogInformation("{Request} - Validation successful.", requestName);
            }
            return await next();
        }
    }
}
