using Microsoft.EntityFrameworkCore;

namespace TaskTrackerService.Data.Storage;

public class TaskTrackerDb : DbContext
{
    public DbSet<PopugTask> Tasks => Set<PopugTask>();

    public DbSet<User> Users => Set<User>();

    protected readonly IConfiguration Configuration;

    public TaskTrackerDb(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("TaskTrackerServiceDb"));
    }
}