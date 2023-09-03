using AccountingService.Data.Storage;
using AccountingService.Queue;

public interface IUserService
{

    Task<User[]> GetUsers();
    
    Task CreateUser(UserCreatedEvent model);

    Task UnassignUser(Guid taskId);

    Task AssignUser(Guid taskId, Guid userId);
}