using Microsoft.EntityFrameworkCore;

namespace TaskTrackerService.Data.Storage;

public class TaskTrackerDb : DbContext
{
    public TaskTrackerDb(DbContextOptions<TaskTrackerDb> options) : base(options) { }

    public DbSet<PopugTask> Tasks => Set<PopugTask>();

    public DbSet<User> Users => Set<User>();
}