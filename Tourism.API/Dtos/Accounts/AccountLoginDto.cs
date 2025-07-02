using System.ComponentModel.DataAnnotations;

namespace Tourism.Api.Dtos.Accounts;

public class AccountLoginDto
{
    [Required, MaxLength(50), MinLength(1), EmailAddress]
    public string Email { get; set; } = String.Empty;

    [Required, MinLength(1)]
    public string Password { get; set; } = String.Empty;
}
