using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Dtos.Accounts;
using Tourism.Api.Interfaces;

namespace Tourism.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> ResultAsync([FromBody] AccountRegisterDto dto)
        =>Ok(await _accountService.RegisterAsync(dto));

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] AccountLoginDto dto)
        => Ok(await _accountService.LoginAsync(dto));
}
