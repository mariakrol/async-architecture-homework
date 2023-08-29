using AccountingService.Queue;

public interface IUserService
{
    Task CreateUser(UserCreatedEvent model);
}