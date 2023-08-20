using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Data.RequestResponseModels.Task;
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

    public async Task<Guid> GetUserIdToAssign(TaskCreationRequest model)
    {
        var usersCount = await _context.Users.CountAsync();

        var randomUser = _random.Next(0, usersCount);

        return _context.Users.ElementAt(randomUser).Id;
    }
}