using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyShortenterApi.Behaviours;
using MyShortenterApi.Repositories;
using MyShortenterApi.Services;
using System;

namespace MyShortenterApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(config =>
            {
                config.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                });
            });
            
            services.AddControllers();
            services.AddBehaviours();
            services.AddServices();
            services.AddRepositories();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyShortener API",
                    Description = "A simple url shortener API",
                    TermsOfService = new Uri("https://github.com/mdyrcz4"),
                    Contact = new OpenApiContact
                    {
                        Name = "Marcin Dyrcz",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/mdyrcz4"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://github.com/mdyrcz4"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
