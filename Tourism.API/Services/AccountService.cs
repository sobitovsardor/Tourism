using Microsoft.EntityFrameworkCore;
using System.Net;
using Tourism.Api.Common.DbContexts;
using Tourism.Api.Common.Exceptions;
using Tourism.Api.Common.Security;
using Tourism.Api.Dtos.Accounts;
using Tourism.Api.Interfaces;
using Tourism.API.Models;

namespace Tourism.Api.Services;

public class AccountService : IAccountService
{
    private AppDbContext _repository;
    private IAuthManager _authManager;

    public AccountService(AppDbContext appDbContext, IAuthManager authManager)
    {
        this._repository = appDbContext;
        this._authManager = authManager;
    }

    public async Task<string> LoginAsync(AccountLoginDto dto)
    {
        var user = await _repository.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "User with this email does not exist.");

        var hasherResult = PasswordHasher.Verify(dto.Password, user.PasswordHash, user.Salt);
        if (hasherResult)
            return _authManager.GenerateToken(user);
        else
            throw new StatusCodeException(HttpStatusCode.Unauthorized, "Invalid password.");
    }

    public async Task<bool> RegisterAsync(AccountRegisterDto dto)
    {
        var emaildUser = await _repository.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if(emaildUser is not null)
            throw new StatusCodeException(HttpStatusCode.Conflict, "User with this email already exists.");

        var hasherResult = PasswordHasher.Hash(dto.Password);
        var userEntity = (User)dto;
        userEntity.PasswordHash = hasherResult.Hash;
        userEntity.Salt = hasherResult.Salt;

        _repository.Users.Add(userEntity);
        var dbResult = await _repository.SaveChangesAsync();
        return dbResult > 0;
    }
}
