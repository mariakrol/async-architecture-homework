using AccountingService.Data.Storage;
using Microsoft.EntityFrameworkCore;

public class AccountingDb : DbContext
{
    public AccountingDb(DbContextOptions<AccountingDb> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<User> Users => Set<User>();
}