using System;
using System.Net;

using AutoMapper;

using Messenger.API.Helpers;
using Messenger.API.Hubs;
using Messenger.Data.Helpers;
using Messenger.Data.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Messenger.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Messenger API",
                    Description = "A test task for koshelek.ru",
                    Contact = new OpenApiContact
                    {
                        Name = "Vyacheslav Lasukov",
                        Email = "lasukovv902@gmail.com",
                        Url = new Uri("https://vk.com/laska_sl")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "No license :(",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                c.EnableAnnotations();
            });

            services.AddCors();

            services.AddSignalR();

            services.AddSingleton<IMessageService, MessageRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messenger API");
            });

            app.UseCors(options =>
                options
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/NewMessage");
            });
        }
    }
}
