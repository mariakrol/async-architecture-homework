using TaskTrackerService.Data.Storage;
using TaskTrackerService.Queue;

namespace TaskTrackerService.Services;

public interface IUserService
{
    Task<Guid> SaveUser(UserCreatedEvent model);

    Task<User[]> GetUsers();
}