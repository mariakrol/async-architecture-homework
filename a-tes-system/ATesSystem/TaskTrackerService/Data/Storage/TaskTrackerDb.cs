using Microsoft.EntityFrameworkCore;

namespace TaskTrackerService.Data.Storage;

public class TaskTrackerDb : DbContext
{
    public TaskTrackerDb(DbContextOptions<TaskTrackerDb> options) : base(options) { }

    public DbSet<Task> Tasks => Set<Task>();

    public DbSet<User> Users => Set<User>();
}