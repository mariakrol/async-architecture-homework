using System.Text.Json.Serialization;
using AccountingService.Data.Configuration;
using AccountingService.Middleware;
using AccountingService.Queue;
using AccountingService.Services;
using AccountingService.Utilities.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Producer;
using TaskTrackerService.Queue;

namespace AccountingService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AccountingDb>();

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

        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddHostedService<UserCreationEventConsumer>();
        services.AddHostedService<UserAssigmentEventConsumer>();
        services.AddHostedService<UserUnassigmentEventConsumer>();

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Task Tracker API",
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