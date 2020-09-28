using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MyShortenterApi.Behaviours
{
    public static class DependencyInjection
    {
        public static void AddBehaviours(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}