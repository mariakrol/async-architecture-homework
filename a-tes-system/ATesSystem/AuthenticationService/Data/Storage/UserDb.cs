using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data.Storage;

public class UserDb : DbContext
{
    public DbSet<User> Users => Set<User>();

    protected readonly IConfiguration Configuration;

    public UserDb(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("AuthServiceDb"));
    }
}