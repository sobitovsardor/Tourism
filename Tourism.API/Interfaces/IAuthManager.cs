using Tourism.API.Models;

namespace Tourism.Api.Interfaces;

public interface IAuthManager
{
    public string GenerateToken(User user);
}
