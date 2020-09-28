using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyShortenterApi.Services.Command;
using MyShortenterApi.Services.Interfaces;
using MyShortenterApi.Services.Query;

namespace MyShortenterApi.Services
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Add MediatR - This adds all of the command and query handlers
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            // Add the request validators
            services.AddTransient<IValidator<GetUrlByKeyQuery>, GetUrlByKeyQueryValidator>();
            services.AddTransient<IValidator<ShortenUrlCommand>, ShortenUrlCommandValidator>();
        }
    }
}
