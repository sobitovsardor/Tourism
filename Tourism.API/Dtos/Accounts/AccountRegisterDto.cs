using System.ComponentModel.DataAnnotations;
using Tourism.API.Models;

namespace Tourism.Api.Dtos.Accounts;

public class AccountRegisterDto
{
    [Required, MaxLength(50), MinLength(1)]
    public string FullName { get; set; } = null!;

    [Required, MaxLength(50), MinLength(1), EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MinLength(1), ]
    public string Password { get; set; } = String.Empty;


    public static implicit operator User(AccountRegisterDto dto)
    {
        return new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
        };
    }
}
