using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Services;

public class WorkerSelectionService : IWorkerSelectionService
{
    private readonly TaskTrackerDb _context;

    private readonly Random _random;

    public WorkerSelectionService(TaskTrackerDb context)
    {
        _context = context;
        _random = new Random();
    }

    public async Task<Guid> GetUserIdToAssign(WorkerAssignRequest request)
    {
        var availableUsers = from user in _context.Users
                                where user.Id != request.PreviousAssignee
                                select user;

        var usersCount = await availableUsers.CountAsync();
        var index = _random.Next(usersCount);
        
        Console.WriteLine($"Users count: {usersCount}, selected: {index}"); //ToDo: log

        if(index == 0) {
            return (await _context.Users.FirstAsync()).Id;
        }
        
        return _context.Users.Skip(index).First().Id;
    }
}