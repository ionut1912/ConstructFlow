using ConstructFlow.Auth.Domain.Entities;
using ConstructFlow.Auth.Domain.Models;

namespace ConstructFlow.Auth.Domain.Abstractions.Services;

public interface ITokenService
{
    AuthResult GenerateToken(Account account);
    RefreshToken GenerateRefreshToken(Guid userId, string jwtToken);
}
