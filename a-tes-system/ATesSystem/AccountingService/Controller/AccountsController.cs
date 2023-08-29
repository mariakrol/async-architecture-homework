using Microsoft.AspNetCore.Mvc;

namespace AccountingService.Controller;

/// <summary>
/// The controller is created just for simplifying of check that consuming of
/// Kafka event works properly
/// </summary>
[ApiController]
[Route("[Controller]")]
public class AccountsController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<Account[]> GetAccounts()
    {
        return await _accountService.GetAccounts();
    }
}