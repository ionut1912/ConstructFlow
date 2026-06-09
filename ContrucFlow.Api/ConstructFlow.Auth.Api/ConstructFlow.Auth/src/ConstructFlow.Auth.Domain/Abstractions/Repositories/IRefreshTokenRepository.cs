using ConstructFlow.Auth.Domain.Entities;
using Shared.Domain.Interfaces;

namespace ConstructFlow.Auth.Domain.Abstractions.Repositories;


public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetExistingTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
