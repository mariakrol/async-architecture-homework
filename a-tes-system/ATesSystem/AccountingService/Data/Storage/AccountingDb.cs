using AccountingService.Data.Storage;
using Microsoft.EntityFrameworkCore;

public class AccountingDb : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<PopugTask> Tasks => Set<PopugTask>();

    public DbSet<User> Users => Set<User>();

    protected readonly IConfiguration Configuration;

    public AccountingDb(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("AccountingServiceDb"));
    }
}