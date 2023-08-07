using AuthenticationService.Data.Configuration;
using AuthenticationService.Data.Storage;
using AuthenticationService.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
        services.AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("Users"));

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.IncludeFields = true;
        });

        services.AddControllers();

        services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

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
        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseSwagger(c =>
        {
            c.SerializeAsV2 = true;
        });

        app.UseSwaggerUI();
        var serviceScope = app.ApplicationServices.CreateScope();
        var userDb = serviceScope.ServiceProvider.GetRequiredService<UserDb>();
        var appSettings = serviceScope.ServiceProvider.GetService<IOptions<AppSettings>>().Value;

        AddTestData(userDb, appSettings);
    }

    private static void AddTestData(UserDb db, AppSettings appSettings)
    {
        var secret = appSettings.PasswordEncryptionSecret;

        var user1 = new User
        {
            Id = Guid.NewGuid(),
            Name = "User 1",
            EncryptedPassword = Encryptor.Encrypt("Password", secret),
            Roles = new[] { Role.Worker }
        };

        var user2 = new User
        {
            Id = Guid.NewGuid(),
            Name = "User 2",
            EncryptedPassword = Encryptor.Encrypt("Password", secret),
            Roles = new[] { Role.Admin }
        };

        db.Add(user1);
        db.Add(user2);
        db.SaveChanges();
    }
}
