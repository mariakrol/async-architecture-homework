using AccountingService.Data.Storage;
using AccountingService.Queue;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly AccountingDb _dataContext;

    public UserService(AccountingDb dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<User[]> GetUsers()
    {
        return await _dataContext.Users.ToArrayAsync();
    }

    public async Task CreateUser(UserCreatedEvent model)
    {
        var newUser = new User(model.Id, model.Name, model.Role);
        _dataContext.Users.Add(newUser);

        await _dataContext.SaveChangesAsync();
    }

    public async Task UnassignUser(Guid taskId)
    {
        var task = await _dataContext.Tasks.FindAsync(taskId);
        task.UnassignUser();

        await _dataContext.SaveChangesAsync();
    }

    public async Task AssignUser(Guid taskId, Guid userId)
    {
        var task = await _dataContext.Tasks.FindAsync(taskId);
        task.AssignUser(userId);

        await _dataContext.SaveChangesAsync();
    }
}