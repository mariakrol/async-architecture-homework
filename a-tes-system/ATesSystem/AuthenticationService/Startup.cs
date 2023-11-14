using AuthenticationService.Data.Configuration;
using AuthenticationService.Data.Storage;
using AuthenticationService.Middleware;
using AuthenticationService.Queue;
using AuthenticationService.Services;
using AuthenticationService.Utilities.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Producer;

namespace AuthenticationService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<UserDb>();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.IncludeFields = true;
        });

        services.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });


        services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        services.Configure<PopugKafkaSettings>(Configuration.GetSection("PopugKafkaSettings"));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, Services.AuthenticationService>();
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<IMessageQueueEventProducerService,  AuthServiceMessageProducer>();

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Authentication API",
                Version = "v1"
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");
        }

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseMiddleware<JwtMiddleware>();

        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseSwagger(c => { c.SerializeAsV2 = true; });
        app.UseSwaggerUI();
    }
}
