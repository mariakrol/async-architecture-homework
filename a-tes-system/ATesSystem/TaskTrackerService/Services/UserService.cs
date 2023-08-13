using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskTrackerService.Data.Storage;
using TaskTrackerService.Queue;
using Task = System.Threading.Tasks.Task;

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
        var user = new User(model.Id, model.UserName, model.Role);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<User[]> GetUsers()
    {
        return await _context.Users.ToArrayAsync();
    }
}