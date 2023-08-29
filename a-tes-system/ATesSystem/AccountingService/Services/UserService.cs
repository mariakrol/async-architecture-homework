using AccountingService.Data.Storage;
using AccountingService.Queue;

public class UserService : IUserService
{
    private readonly AccountingDb _dataContext;

    public UserService(AccountingDb dataContext)
    {
        _dataContext = dataContext;
    }


    public async Task CreateUser(UserCreatedEvent model)
    {
        var newUser = new User(model.Id, model.Name, model.Role);
        _dataContext.Users.Add(newUser);

        await _dataContext.SaveChangesAsync();
    }
}