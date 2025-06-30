﻿using System.ComponentModel.DataAnnotations;

namespace Tourism.Api.Dtos.Accounts;

public class AccountLoginDto
{
    [Required, MaxLength(30), MinLength(2), EmailAddress]
    public string Email { get; set; } = String.Empty;

    [Required, MinLength(8)]
    public string Password { get; set; } = String.Empty;
}
