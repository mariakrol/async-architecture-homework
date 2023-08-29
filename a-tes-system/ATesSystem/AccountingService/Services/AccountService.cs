using Microsoft.EntityFrameworkCore;

namespace AccountingService.Services;

public class AccountService : IAccountService
{
    private readonly AccountingDb _dataContext;

    public AccountService(AccountingDb dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Account> CreateAccount(Guid userId)
    {
        var newAccount = new Account(Guid.NewGuid(), userId);
        _dataContext.Accounts.Add(newAccount);
        await _dataContext.SaveChangesAsync();

        return newAccount;
    }

    public async Task SetAssigmentFee(Guid userId, int fee)
    {
        await ChangeBalance(userId, fee);

        // Produce event
    }

    public async Task SetFinalizationReward(Guid userId, int reward)
    {
        await ChangeBalance(userId, reward);

        // produce event
    }

    private async Task ChangeBalance(Guid userId, int fee)
    {
        var account = _dataContext.Accounts.First(a => a.UserId == userId);
        account.ChangeBalance(fee);

        await _dataContext.SaveChangesAsync();
    }

    public async Task<Account[]> GetAccounts() {
         return await _dataContext.Accounts.ToArrayAsync();
    }
}