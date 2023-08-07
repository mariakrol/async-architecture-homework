﻿using AuthenticationService.Data.Configuration;
using AuthenticationService.Data.Storage;
using AuthenticationService.Utilities;
using Microsoft.Extensions.Options;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Services;

internal class UserService : IUserService
{
    private readonly UserDb _context;
    private readonly AppSettings _appSettings;

    public UserService(
        UserDb context,
        IOptions<AppSettings> appSettings)
    {
        _context = context;
        _appSettings = appSettings.Value;
    }

    public async Task<User> CreateUser(string name, string password, Role[] roles)
    {
        var isExists = await _context.Users.AnyAsync(user => user.Name.Equals(name));

        if (isExists)
        {
            throw new InvalidOperationException($"User with the name '{name}' is already exists");
        }
        var secret = _appSettings.PasswordEncryptionSecret;

        var user = new User(id: Guid.NewGuid(), name: name, encryptedPassword: Encryptor.Encrypt(password, secret), roles);
        
        _context.Users.Add(user);

        return user;
    }

    public async Task<User> RetrieveUser(string name, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.Name.Equals(name));

        if (user is null)
        {
            throw new ArgumentException($"User with the name '{name}' is not exists");
        }
        var secret = _appSettings.PasswordEncryptionSecret;

        var encryptedPassword = Encryptor.Encrypt(password, secret);

        if (user.EncryptedPassword.Equals(encryptedPassword))
        {
            return user;
        }

        throw new ArgumentException($"User with the name '{name}' is found but password is unexpected");
    }
}