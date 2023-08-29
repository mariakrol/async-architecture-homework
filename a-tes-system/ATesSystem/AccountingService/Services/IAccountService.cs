public interface IAccountService
{
    Task<Account> CreateAccount(Guid userId);

    Task SetAssigmentFee(Guid userId, int fee);

    Task SetFinalizationReward(Guid userId, int reward);

    Task<Account[]> GetAccounts();
}
