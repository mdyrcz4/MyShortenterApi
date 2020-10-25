using Microsoft.Extensions.DependencyInjection;
using MyShortenterApi.Repositories.Interfaces;
using StackExchange.Redis;
using System;

namespace MyShortenterApi.Repositories
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                services.AddSingleton<IRepository, MockRepository>();
            else
            {
                var redisEndpoint = Environment.GetEnvironmentVariable("REDISCLOUD_URL");
                var redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");

                services.AddSingleton(ConnectionMultiplexer.Connect($"{redisEndpoint},password={redisPassword}"));
                services.AddTransient<IRepository, RedisRepository>();
            }
        }
    }
}
