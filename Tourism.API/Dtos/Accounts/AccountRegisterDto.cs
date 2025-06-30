using System.ComponentModel.DataAnnotations;
using Tourism.API.Models;

namespace Tourism.Api.Dtos.Accounts;

public class AccountRegisterDto
{
    [Required, MaxLength(30), MinLength(2)]
    public string FullName { get; set; } = null!;

    [Required, MaxLength(30), MinLength(2)]
    public string Email { get; set; } = null!;

    [Required, MinLength(8)]
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
