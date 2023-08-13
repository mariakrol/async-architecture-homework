using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PopugKafkaClient.Data.Configuration;
using TaskTrackerService.Data.Storage;
using TaskTrackerService.Middleware;
using TaskTrackerService.Queue;
using TaskTrackerService.Services;

namespace TaskTrackerService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<TaskTrackerDb>(opt => opt.UseInMemoryDatabase("TaskTracker"));

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.IncludeFields = true;
        });

        services.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.Configure<PopugKafkaSettings>(Configuration.GetSection("PopugKafkaSettings"));

        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IWorkerSelectionService, WorkerSelectionService>();
        services.AddScoped<ICostCalculatorService, CostCalculatorService>();
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<UserEventConsumer>();

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

        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseSwagger(c => { c.SerializeAsV2 = true; });
        app.UseSwaggerUI();
    }
}