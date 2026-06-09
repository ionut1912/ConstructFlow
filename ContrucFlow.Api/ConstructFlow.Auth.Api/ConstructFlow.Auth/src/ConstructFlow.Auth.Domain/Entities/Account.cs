using ConstructFlow.Auth.Domain.Abstractions.Services;
using ConstructFlow.Auth.Domain.Exceptions;
using ConstructFlow.Auth.Domain.ValueObjects;
using Shared.Domain.Common;

namespace ConstructFlow.Auth.Domain.Entities;

public class Account : Entity
{
    private Account() //for EF core
    {
        Role= Role.ClientReadonly;
    }

    private Account(string email, string passwod, string username, string name, string role)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidFieldException($"{nameof(Email)} is invalid");
        }

        if (string.IsNullOrWhiteSpace(passwod))
        {
            throw new InvalidFieldException($"{nameof(Password)} is invalid");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidFieldException($"{nameof(Username)} is invalid");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidFieldException($"{nameof(Name)} is invalid");
        }
        if (string.IsNullOrWhiteSpace(role))
        {
            throw new InvalidFieldException($"{nameof(Role)} is invalid");
        }

        Email = email;
        Password = passwod;
        Username = username;
        Name = name;
        Role = Role.FromString(role);
        CreatedAt = DateTime.UtcNow;
    }

    public static Account Create(
        string email,
        string plainPassword,
        string username,
        string name,
        string role,
        IPasswordService passwordService)
    {
        ArgumentNullException.ThrowIfNull(passwordService);

        var hashedPassword = passwordService.Hash(plainPassword);
        return new Account(email, hashedPassword, username, name, role);
    }

    public void ChangePassword(string newPlainPassword, IPasswordService passwordService)
    {
        if (string.IsNullOrWhiteSpace(newPlainPassword))
            throw new InvalidFieldException($"{nameof(Password)} is invalid");

        Password = passwordService.Hash(newPlainPassword);
        UpdatedAt = DateTime.UtcNow;
    }

    public bool VerifyPassword(string plainPassword, IPasswordService passwordService)
    => passwordService.Verify(plainPassword, Password);

    public void SetResetPasswordExpiresAt(DateTime expiresAt) => ResetPasswordTokenExpiresAt = expiresAt;

    public void GenerateResetPasswordToken(string token, IPasswordService passwordService)
    {
        if (string.IsNullOrWhiteSpace(token)) throw new InvalidFieldException("Token-ul este invalid");

        ResetPasswordToken = passwordService.Hash(token);
        ResetPasswordTokenExpiresAt = DateTime.UtcNow.AddHours(1);
    }

    public void ResetPasswordTokenAndExpiry()
    {
        ResetPasswordToken = string.Empty;
        ResetPasswordTokenExpiresAt = DateTime.UtcNow;
    }

    public void ChangeRole(string role)
    {
        if (string.IsNullOrEmpty(role))
        {

            throw new InvalidFieldException($"{nameof(Role)} is invalid");
        }

        Role = Role.FromString(role);
        UpdatedAt = DateTime.UtcNow;
    }

    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string ResetPasswordToken { get; private set; } = string.Empty;
    public DateTime ResetPasswordTokenExpiresAt { get; private set; }
    public Role Role { get; private set; }

}
