using ConstructFlow.Auth.Domain.Abstractions.Services;

namespace ConstructFlow.Auth.Infrastructure.Services;

public class PasswordService : IPasswordService
{

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hashed)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashed);
    }
}
