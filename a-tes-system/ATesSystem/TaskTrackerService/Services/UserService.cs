using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Data.Storage;
using TaskTrackerService.Queue;

namespace TaskTrackerService.Services;

public class UserService : IUserService
{
    private readonly TaskTrackerDb _context;

    public UserService(TaskTrackerDb context)
    {
        _context = context;
    }

    public async Task<Guid> SaveUser(UserCreatedEvent model)
    {
        Console.WriteLine($"User to be saved in Tracker: {model.Id}; {model.Name}, {model.Role}"); // ToDo: Log

        var user = new User(model.Id, model.Name, model.Role);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<User[]> GetUsers()
    {
        return await _context.Users.ToArrayAsync();
    }
}