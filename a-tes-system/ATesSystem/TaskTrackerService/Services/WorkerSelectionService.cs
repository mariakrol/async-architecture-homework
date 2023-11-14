using AuthenticationService.Queue;
using Microsoft.EntityFrameworkCore;
using PopugKafkaClient.Producer;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Services;

public class WorkerSelectionService : IWorkerSelectionService
{
    private readonly TaskTrackerDb _context;
    private readonly IMessageQueueEventProducerService _queueEventProducer;
    private readonly Random _random;

    public WorkerSelectionService(TaskTrackerDb context, IMessageQueueEventProducerService queueEventProducer)
    {
        _context = context;
        _queueEventProducer = queueEventProducer;
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

        var newAssigneeId = index == 0
            ? (await _context.Users.FirstAsync()).Id
            : (await _context.Users.Skip(index).FirstAsync()).Id;
        
        if (request.PreviousAssignee is not null)
        {
            await _queueEventProducer.Produce("user-assigning-stream",
                new UserUnassignedEvent(
                    new AssigmentChangeEventPayload(request.TaskId)));
        }

        await _queueEventProducer.Produce("user-assigning-stream",
                new UserAssignedEvent(
                    new UserAssignedEventPayload(request.TaskId, newAssigneeId)));

        return newAssigneeId;
    }
}