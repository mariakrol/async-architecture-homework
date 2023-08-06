using AuthenticationService.Data.Storage;
using Microsoft.EntityFrameworkCore;

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
    }
}
