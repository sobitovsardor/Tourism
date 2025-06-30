using Tourism.Api.Dtos.Accounts;

namespace Tourism.Api.Interfaces;

public interface IAccountService
{
    public Task<bool> RegisterAsync(AccountRegisterDto dto);

    public Task<string> LoginAsync(AccountLoginDto dto);
}
